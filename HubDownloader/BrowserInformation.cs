using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HubDownloader
{
    public enum BrowserSelection
    {
        Chrome = 0,
        Edge = 1,
        Firefox = 2
    }

    public static class BrowserInformation
    {
        public static class RegistryLocation
        {
            public static string Chrome = @"HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command";
            public static string Edge = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\microsoft-edge\shell\open\command";
            public static string FireFox64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\FirefoxHTML-308046B0AF4A39CB\shell\open\command";
            public static string FireFox32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\FirefoxHTML-E7CF176E110C211B\shell\open\command";
            public static string StartMenuInternet = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Clients\StartMenuInternet\{0]\shell\open\command";
        }

        /// <summary>
        /// {0} == URL
        /// </summary>
        public static class CommandLineArguments
        {
            public static string Chrome = "{0} -incognito";
            public static string Edge = "{0} -inprivate";
            public static string FireFox = "-private -url {0}";
        }

        public static class ExecutableLocation
        {
            private static string FromRegistry(string keyName)
            {
                string result = Registry.GetValue(keyName, null, null) as string;
                if (result != null)
                {
                    result = result.Split('"', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).First();
                }
                return result;
            }

            public static string GetChrome()
            {
                return FromRegistry(RegistryLocation.Chrome);
            }

            public static string GetEddge()
            {
                return FromRegistry(RegistryLocation.Edge);
            }

            public static string GetFireFox()
            {
                // Of course, Firefox has to be difficult
                string latestVersionPath64bit = FromRegistry(RegistryLocation.FireFox64);
                if (latestVersionPath64bit != null)
                {
                    return latestVersionPath64bit;
                }

                string keyToVersion = @"HKEY_LOCAL_MACHINE\SOFTWARE\mozilla.org\Mozilla";
                string? ffVersion = Registry.GetValue(keyToVersion, "CurrentVersion", null) as string;
                string keyFormatString = @"HKEY_LOCAL_MACHINE\SOFTWARE\Mozilla\Mozilla Firefox {0}\bin";
                string? result = Registry.GetValue(string.Format(keyFormatString, ffVersion), "PathToExe", null) as string;
                return result;
            }
        }
    }
}
