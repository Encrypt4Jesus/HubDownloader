using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader.Debug
{
    public static class Log
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        private static bool IsLoggingEnabled()
        {
            if (Settings.CurrentlyLoadedSettings.EnableDebugLogging)
            {
                return true;
            }

            if (IsDebug())
            {
                return true;
            }
            return false;
        }

        public static void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public static void WriteLine(string line)
        {
            if (IsLoggingEnabled())
            {
                File.AppendAllText(Settings.CurrentlyLoadedSettings.DebuggingLogFile, line + Environment.NewLine);
            }
        }

        public static void Clear()
        {
            if (IsLoggingEnabled())
            {
                File.WriteAllText(Settings.CurrentlyLoadedSettings.DebuggingLogFile, string.Empty);
            }
        }

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.ffff'");
        }
    }
}
