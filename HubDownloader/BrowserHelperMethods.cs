using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public static class BrowserHelperMethods
    {
        public static void LaunchPreferredBrowser(string url, bool openInPrivateMode)
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

            if (openInPrivateMode)
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
    }
}
