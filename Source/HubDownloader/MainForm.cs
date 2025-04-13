using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Reflection;
using System.Security.Policy;
using OpenQA.Selenium.BiDi.Modules.Script;

namespace HubDownloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            panelProcessing.Visible = false;
            flpOutput_Links.Controls.Clear();
            ViewKeyQueue = new ConcurrentQueue<string>();
            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;

            SetEnvironmentVariables();
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            richTextBox_Example.Select(34, 13);
            richTextBox_Example.SelectionFont = new Font(richTextBox_Example.Font, FontStyle.Bold);
            richTextBox_Example.DeselectAll();

            Settings.Load();

            Activated += MainForm_Activated;
            Enter += MainForm_Enter;

            UrlExtractionWorkflow.NewResultsAvailable += UrlExtractionWorkflow_NewResultsAvailable;
            EnableQueueProcessor();
        }

        private void SetEnvironmentVariables()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string managerPath = Path.Combine(dir, "selenium-manager.exe");

           Environment.SetEnvironmentVariable("SE_MANAGER_PATH", managerPath, EnvironmentVariableTarget.User);
        }

        private void MainForm_Enter(object? sender, EventArgs e)
        {
            InspectClipboard();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            InspectClipboard();
        }

        private void InspectClipboard()
        {
            // When the form is the active form receiving focus again,
            // it is likely the user intends to paste a viewkey into the application.
            // So check if the clipboard contains a CornHub URL or a string that resembles a view key
            // and enter that value for the user, if the feature is enabled.

            if (!Settings.CurrentlyLoadedSettings.AutoScanClipboard)
            {
                return;
            }
            if (!Clipboard.ContainsText())
            {
                return;
            }

            string clipboardText = Clipboard.GetText();

            if (string.IsNullOrEmpty(clipboardText))
            {
                return;
            }

            if (!UrlExtractionHelperMethods.LooksLikeUrlOrViewkey(clipboardText))
            {
                return;
            }

            Clipboard.Clear();
            tbInput_ViewKeys.Text = clipboardText;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            UrlExtractionWorkflow.Shutdown();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.SelectedSettings = Settings.CurrentlyLoadedSettings;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                Settings.CurrentlyLoadedSettings = settingsForm.SelectedSettings;
                Settings.Save();
            }
        }

        #region Task Queuing

        protected ConcurrentQueue<string> ViewKeyQueue;

        private void tbInput_ViewKeys_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbInput_ViewKeys.Text))
            {
                MoveInputToQueue();
            }
        }

        private void tbInput_ViewKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MoveInputToQueue();
            }
        }

        private void MoveInputToQueue()
        {
            if (string.IsNullOrWhiteSpace(tbInput_ViewKeys.Text))
            {
                return;
            }

            string input = tbInput_ViewKeys.Text.Trim();

            input = UrlExtractionHelperMethods.IsolateViewkey(input);

            tbInput_ViewKeys.Clear();
            ViewKeyQueue.Enqueue(input); ;
            SetProcessingUI(input);
            EnableQueueProcessor();
        }

        private void SetProcessingUI(string viewKey = null)
        {
            if (!string.IsNullOrWhiteSpace(viewKey))
            {
                labelProcessing.Text = string.Format("Processing '{0}'...", viewKey);
                panelProcessing.Visible = true;
            }
            else
            {
                panelProcessing.Visible = false;
                labelProcessing.Text = "";
            }
        }

        private void timerInputToQueueProcessor_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbInput_ViewKeys.Text))
            {
                MoveInputToQueue();
            }
        }

        private void EnableQueueProcessor()
        {
            if (!timerQueueProcessor.Enabled)
            {
                this.Invoke(() => timerQueueProcessor.Enabled = true);
            }
        }

        private void DisableQueueProcessor()
        {
            if (timerQueueProcessor.Enabled)
            {
                this.Invoke(() => timerQueueProcessor.Enabled = false);
            }
        }

        private void timerQueueProcessor_Tick(object sender, EventArgs e)
        {
            if (UrlExtractionWorkflow.IsProcessing)
            {
                DisableQueueProcessor();
                return;
            }

            string input = DequeueInput();
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            DisableQueueProcessor();

            UrlExtractionResultObject result = null;
            Thread newThread = new Thread(new ThreadStart(() =>
            {
                result = UrlExtractionWorkflow.ExtractUrlFromViewKey(input, Settings.CurrentlyLoadedSettings.PreferredVideoQuality, Settings.CurrentlyLoadedSettings.FallbackVideoQuality);
            }));
            newThread.Start();
        }

        private string DequeueInput()
        {
            if (ViewKeyQueue.TryDequeue(out string result))
            {
                return result;
            }

            return string.Empty;
        }

        #endregion

        #region LinkLabel Events

        public void LaunchOnUiThread(Action action)
        {
            this.Invoke(action);
        }

        private void UrlExtractionWorkflow_NewResultsAvailable(object? sender, NewResultsAvailableEventArgs e)
        {
            LaunchOnUiThread(new Action(() =>
            {
                UrlExtractionResultObject localCopy = e.Results;
                NewResultsAvailable_Handler(localCopy);
            }));
        }

        private void NewResultsAvailable_Handler(UrlExtractionResultObject result)
        {
            if (!result.IsSuccess)
            {
                Debug.Log.WriteLine($"<ERROR>");
                Debug.Log.WriteLine($"    {nameof(UrlExtractionWorkflow)}.{nameof(UrlExtractionWorkflow.ExtractUrlFromViewKey)} failed with following error reason: '{Enum.GetName(typeof(ExtractionWorkflowResult), result.ErrorReason)}'.");
                Debug.Log.WriteLine($"    <ERROR.DETAILS>");
                if (result.DebugInformation.Length > 0)
                {
                    string debugBuffer = result.DebugInformation.ToString();
                    string[] debugLines = debugBuffer.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in debugLines)
                    {
                        Debug.Log.WriteLine($"        " + line);
                    }
                }
                Debug.Log.WriteLine($"    </ERROR.DETAILS>");
                Debug.Log.WriteLine($"</ERROR>");
                return;
            }

            string linkText = result.Name;

            if (Settings.CurrentlyLoadedSettings.DiscreetLinkTitles)
            {
                // https://ev.phncdn.com/videos/202503/14/465748905/1080P_4000K_465748905.mp4?validfrom=1743789186&validto=1743796386&rate=50000k&burst=50000k&ip=47.5.67.113&ipa=47.5.67.113&hash=h1QudCHMKHofBotql+8qcnrDbvQ=

                // Possible strategies:
                // - Use URI filename, i.e. 1080P_4000K_465748905
                // - Use URI hash, i.e. h1QudCHMKHofBotql+8qcnrDbvQ=
                // - Take the first characters from every word in the title, i.e. Tcom2r or T-c-o-m-2-r
                // - Take the first 2 characters from every word in the title, i.e. Tacaofmy2ro or Ta-ca-of-my-2-ro
                // - A counter + timestamp, i.e. #4 - 8:22:13 PM

                Uri uri = new Uri(result.Url);
                linkText = uri.Segments.Last();
            }

            LinkLabel resultLink = new LinkLabel()
            {
                Text = linkText,
                Tag = result,
                LinkBehavior = LinkBehavior.AlwaysUnderline,
                Margin = new Padding(10),
                AutoSize = true
            };
            resultLink.MouseClick += ResultLink_MouseClick;
            resultLink.ContextMenuStrip = linkContextMenu;

            toolTip.SetToolTip(resultLink, result.Url);

            flpOutput_Links.Controls.Add(resultLink);
            SetProcessingUI();
            EnableQueueProcessor();
        }

        private static string GetLinkAddress(LinkLabel linklabelControl)
        {
            if (linklabelControl == null)
            {
                throw new ArgumentNullException(nameof(linklabelControl));
            }
            UrlExtractionResultObject resultObject = linklabelControl.Tag as UrlExtractionResultObject;
            if (resultObject == null)
            {
                throw new Exception($"LinkLabel.Tag was null or not a {nameof(UrlExtractionResultObject)} object.");
            }

            return resultObject.Url;
        }

        private void ResultLink_MouseClick(object? sender, MouseEventArgs e)
        {
            LinkLabel linkContrl = (LinkLabel)sender;

            if (e.Button == MouseButtons.Left)
            {
                string linkAddress = GetLinkAddress(linkContrl);

                if (Settings.CurrentlyLoadedSettings.RemoveVisitedLinks)
                {
                    flpOutput_Links.Controls.Remove(linkContrl);
                    linkContrl.Tag = null;
                    linkContrl.Dispose();
                    linkContrl = null;
                }
                else
                {
                    linkContrl.LinkVisited = true;
                }

                BrowserHelperMethods.LaunchPreferredBrowser(linkAddress, Settings.CurrentlyLoadedSettings.OpenBrowserInPrivateMode);
            }
            else if (e.Button == MouseButtons.Right)
            {
                linkContextMenu.Show();
            }
        }

        private void toolStripMenuOpenLink_Click(object sender, EventArgs e)
        {
            // Open
            string linkAddress = GetLinkAddress((LinkLabel)linkContextMenu.SourceControl);
            if (linkAddress != null)
            {
                BrowserHelperMethods.LaunchPreferredBrowser(linkAddress, Settings.CurrentlyLoadedSettings.OpenBrowserInPrivateMode);
            }
        }

        private void toolStripMenuCopyLink_Click(object sender, EventArgs e)
        {
            string linkAddress = GetLinkAddress((LinkLabel)linkContextMenu.SourceControl);
            if (linkAddress != null)
            {
                Clipboard.SetText(linkAddress);
            }
        }

        private void renewLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkLabel linkCtrl = (LinkLabel)linkContextMenu.SourceControl;

            string url = GetLinkAddress(linkCtrl);

            RemoveLinkControl(linkCtrl);

            tbInput_ViewKeys.Text = url;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkLabel ll = (LinkLabel)linkContextMenu.SourceControl;
            RemoveLinkControl(ll);
        }

        private void RemoveLinkControl(LinkLabel linkControl)
        {
            if (linkControl != null)
            {
                flpOutput_Links.Controls.Remove(linkControl);
                linkControl.Dispose();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flpOutput_Links.Controls.Clear();
        }

        #endregion

    }
}
