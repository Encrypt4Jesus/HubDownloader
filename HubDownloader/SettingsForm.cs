using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HubDownloader;

namespace HubDownloader
{
    public partial class SettingsForm : Form
    {
        public Settings SelectedSettings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            this.Shown += SettingsForm_Shown;
        }

        private static Tuple<int, string, BrowserSelection>[] _browserSelectionDict = new Tuple<int, string, BrowserSelection>[]
        {
            new Tuple<int, string, BrowserSelection>(0, "Chrome", BrowserSelection.Chrome ),
            new Tuple<int, string, BrowserSelection>(1, "Edge", BrowserSelection.Edge ),
            new Tuple<int, string, BrowserSelection>(2, "Firefox", BrowserSelection.Firefox )
        };


        private static Tuple<int, string, RequestedVideoQuality>[] _requestedVideoQualityDict = new Tuple<int, string, RequestedVideoQuality>[]
        {
            new Tuple<int, string, RequestedVideoQuality>( 0, "Default", RequestedVideoQuality.Default ),
            new Tuple<int, string, RequestedVideoQuality>( 1, "240p", RequestedVideoQuality.LD_240 ),
            new Tuple<int, string, RequestedVideoQuality>( 2, "480p", RequestedVideoQuality.SD_480 ),
            new Tuple<int, string, RequestedVideoQuality>( 3, "720p", RequestedVideoQuality.ED_720 ),
            new Tuple<int, string, RequestedVideoQuality>( 4, "1080p", RequestedVideoQuality.HD_1080)
        };


        private static Tuple<int, string, FallBackVideoQuality>[] _fallBackVideoQualityDict = new Tuple<int, string, FallBackVideoQuality>[]
        {
            new Tuple<int, string, FallBackVideoQuality>(0,"Lowest", FallBackVideoQuality.Lowest ),
            new Tuple<int, string, FallBackVideoQuality>(1,"Highest", FallBackVideoQuality.Highest )
        };

        private void SettingsForm_Shown(object? sender, EventArgs e)
        {
            tbChromeInstallationLocation.Text = SelectedSettings.ChromeInstalledLocation;
            tbEdgeInstallationLocation.Text = SelectedSettings.EdgeInstalledLocation;
            tbFirefoxInstallationLocation.Text = SelectedSettings.FirefoxInstalledLocation;

            cbPrivateMode.Checked = SelectedSettings.OpenBrowserInPrivateMode;

            comboPreferredBrowser.DataSource = Enum.GetValues(typeof(BrowserSelection));
            comboPreferredVideoQuality.DataSource = Enum.GetValues(typeof(RequestedVideoQuality));
            comboFallbackVideoQuality.DataSource = Enum.GetValues(typeof(FallBackVideoQuality));

            comboPreferredBrowser.SelectedItem = SelectedSettings.PreferredBrowser;
            comboPreferredVideoQuality.SelectedItem = SelectedSettings.PreferredVideoQuality;
            comboFallbackVideoQuality.SelectedItem = SelectedSettings.FallbackVideoQuality;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedSettings.ChromeInstalledLocation = tbChromeInstallationLocation.Text;
            SelectedSettings.EdgeInstalledLocation = tbEdgeInstallationLocation.Text;
            SelectedSettings.FirefoxInstalledLocation = tbFirefoxInstallationLocation.Text;

            SelectedSettings.OpenBrowserInPrivateMode = cbPrivateMode.Checked;

            SelectedSettings.PreferredBrowser = (BrowserSelection)comboPreferredBrowser.SelectedItem;
            SelectedSettings.PreferredVideoQuality = (RequestedVideoQuality)comboPreferredVideoQuality.SelectedItem;
            SelectedSettings.FallbackVideoQuality = (FallBackVideoQuality)comboFallbackVideoQuality.SelectedItem;
        }
    }
}
