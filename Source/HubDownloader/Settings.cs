using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Windows.Forms.LinkLabel;
using static HubDownloader.BrowserInformation;

namespace HubDownloader
{
    public class Settings
    {
        public static string SettingsFile = "Settings.json";
        public static Settings CurrentlyLoadedSettings = null;

        public Settings() { }

        public static void Load()
        {
            if (File.Exists(SettingsFile))
            {
                string json = File.ReadAllText(SettingsFile);
                Settings? loaded = JsonConvert.DeserializeObject<Settings>(json);
                if (loaded == null)
                {
                    throw new Exception($"Error loading settings from file: {SettingsFile}");
                }
                CurrentlyLoadedSettings = loaded;
            }
            else // Create a settings file with default settings
            {
                Settings newSettings = new Settings()
                {
                    EnableDebugLogging = false,
                    DebuggingLogFile = "debugging.log.txt",
                    PreferredBrowser = BrowserSelection.Edge,
                    OpenBrowserInPrivateMode = true,
                    AutoScanClipboard = false,
                    DiscreetLinkTitles = false,
                    RemoveVisitedLinks = false,
                    PreferredVideoQuality = RequestedVideoQuality.ED_720,
                    FallbackVideoQuality = FallBackVideoQuality.Highest,
                    ChromeInstalledLocation = ExecutableLocation.GetChrome(),
                    EdgeInstalledLocation = ExecutableLocation.GetEddge(),
                    FirefoxInstalledLocation = ExecutableLocation.GetFireFox()
                };
                CurrentlyLoadedSettings = newSettings;
                Save();
            }
        }

        public static void Save()
        {
            if (CurrentlyLoadedSettings == null)
            {
                throw new NullReferenceException(nameof(CurrentlyLoadedSettings));
            }
            string json = JsonConvert.SerializeObject(CurrentlyLoadedSettings, Formatting.Indented);
            File.WriteAllText(SettingsFile, json);
        }

        public bool EnableDebugLogging { get; set; }
        public string DebuggingLogFile { get; set; }

        public string ChromeInstalledLocation { get; set; }
        public string EdgeInstalledLocation { get; set; }
        public string FirefoxInstalledLocation { get; set; }

        public BrowserSelection PreferredBrowser { get; set; }

        public bool OpenBrowserInPrivateMode { get; set; }
        public bool AutoScanClipboard { get; set; }
        public bool DiscreetLinkTitles { get; set; }
        public bool RemoveVisitedLinks { get; set; }

        public RequestedVideoQuality PreferredVideoQuality { get; set; }

        public FallBackVideoQuality FallbackVideoQuality { get; set; }
    }
}

