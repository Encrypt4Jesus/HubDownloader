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
        public static string LogFile = InternalSettings.Debug_LogFile;

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public static void WriteLine(string line)
        {
            if (IsDebug())
            {
                File.AppendAllText(LogFile, line + Environment.NewLine);
            }
        }

        public static void Clear()
        {
            if (IsDebug())
            {
                File.WriteAllText(LogFile, string.Empty);
            }
        }

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.ffff'");
        }
    }
}
