using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public static class InternalSettings
    {
        public static int Selenium_ImplicitWaitTime = 20;

        public static string HubAddressFormat = "https://www.pornhub.com/view_video.php?viewkey={0}";

        public static string Debug_LogFile = "debug.log.txt";

        public static string JsonMetadata_SearchToken = "var flashvars_";
        public static string JsonMetadata_XPath = "//*[@id=\"player\"]/script[1]";

        public static string JsonMediaSection_PropertyName = "mediaDefinitions";
        public static string JsonMediaSection_VideoProperty = "videoUrl";
        public static string JsonMediaSection_QualityProperty = "quality";

        public static string MediaAddressDictionary_Key = "[]";

        public static string Json1_Extract_StartToken = "{";
        public static string Json1_Extract_EndToken = "var player_mp4_seek";

        public static string Json2_Extract_StartToken = "[";
        public static string Json2_Extract_EndToken = "]";

    }
}
