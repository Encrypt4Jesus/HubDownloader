using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Security.Policy;

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
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            richTextBox_Example.Select(34, 13);
            richTextBox_Example.SelectionFont = new Font(richTextBox_Example.Font, FontStyle.Bold);
            richTextBox_Example.DeselectAll();

            Settings.Load();

            UrlExtractionWorkflow.NewResultsAvailable += UrlExtractionWorkflow_NewResultsAvailable;
            EnableQueueProcessor();
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
            UrlExtractionResultObject? result = UrlExtractionWorkflow.ExtractUrlFromViewKey(input, Settings.CurrentlyLoadedSettings.PreferredVideoQuality, Settings.CurrentlyLoadedSettings.FallbackVideoQuality);
            if (result == null)
            {
                string errorReason = Enum.GetName(typeof(ExtractionWorkflowResult), UrlExtractionWorkflow.LastErrorReason);
                Debug.Log.WriteLine($"ERROR CAUGHT: {nameof(UrlExtractionWorkflow)}.{nameof(UrlExtractionWorkflow.ExtractUrlFromViewKey)} failed with following error reason: '{errorReason}'.");

                UrlExtractionWorkflow.LastErrorReason = ExtractionWorkflowResult.Success;
            }
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

        private void UrlExtractionWorkflow_NewResultsAvailable(object? sender, NewResultsAvailableEventArgs e)
        {
            UrlExtractionResultObject resultObject = e.Results;
            if (string.IsNullOrWhiteSpace(resultObject.Url))
            {
                return;
            }

            LinkLabel resultLink = new LinkLabel()
            {
                Text = resultObject.Name,
                Tag = resultObject.Url,
                LinkBehavior = LinkBehavior.AlwaysUnderline,
                Margin = new Padding(10),
                AutoSize = true
            };
            resultLink.MouseClick += ResultLink_MouseClick;
            resultLink.ContextMenuStrip = linkContextMenu;

            toolTip.SetToolTip(resultLink, resultObject.Url);

            flpOutput_Links.Controls.Add(resultLink);
            SetProcessingUI();
            EnableQueueProcessor();
        }

        private void ResultLink_MouseClick(object? sender, MouseEventArgs e)
        {
            LinkLabel linkContrl = (LinkLabel)sender;

            if (e.Button == MouseButtons.Left)
            {
                string linkAddress = linkContrl.Tag.ToString();
                LaunchPreferredBrowser(linkAddress);
            }
            else if (e.Button == MouseButtons.Right)
            {
                linkContextMenu.Show();
            }
        }

        private void toolStripMenuOpenLink_Click(object sender, EventArgs e)
        {
            // Open
            string linkAddress = GetLinkAddress();
            if (linkAddress != null)
            {
                LaunchPreferredBrowser(linkAddress);
            }
        }

        private void toolStripMenuCopyLink_Click(object sender, EventArgs e)
        {
            string linkAddress = GetLinkAddress();
            if (linkAddress != null)
            {
                Clipboard.SetText(linkAddress);
            }
        }

        private string GetLinkAddress()
        {
            LinkLabel ll = (LinkLabel)linkContextMenu.SourceControl;
            if (ll == null)
            {
                return null;
            }
            return ll.Tag.ToString();
        }

        private void LaunchPreferredBrowser(string url)
        {
            string browserExecutablePath = "";
            string argument = "";

            if (Settings.CurrentlyLoadedSettings.PreferredBrowser == BrowserSelection.Chrome)
            {
                browserExecutablePath = Settings.CurrentlyLoadedSettings.ChromeInstalledLocation;
                argument = BrowserInformation.CommandLineArguments.Chrome;
            }
            else if (Settings.CurrentlyLoadedSettings.PreferredBrowser == BrowserSelection.Edge)
            {
                browserExecutablePath = Settings.CurrentlyLoadedSettings.EdgeInstalledLocation;
                argument = BrowserInformation.CommandLineArguments.Edge;
            }
            else if (Settings.CurrentlyLoadedSettings.PreferredBrowser == BrowserSelection.Firefox)
            {
                browserExecutablePath = Settings.CurrentlyLoadedSettings.FirefoxInstalledLocation;
                argument = BrowserInformation.CommandLineArguments.FireFox;
            }

            if (Settings.CurrentlyLoadedSettings.OpenBrowserInPrivateMode)
            {
                argument = string.Format(argument, url);
            }
            else
            {
                argument = url;
            }

            Process proc = new Process()
            {
                StartInfo = new ProcessStartInfo(browserExecutablePath, argument)
            };

            proc.Start();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkLabel ll = (LinkLabel)linkContextMenu.SourceControl;
            if (ll != null)
            {
                flpOutput_Links.Controls.Remove(ll);
                ll.Dispose();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flpOutput_Links.Controls.Clear();
        }

        #endregion

    }
}
