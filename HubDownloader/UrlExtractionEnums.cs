using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public enum ExtractionWorkflowResult
    {
        Success,
        NoInternet,
        JsonMetadataNotFound,
        JsonExtractionFailure,
        JsonParseError,
        QualityNotAvailable,
        Http404,
        Captcha,
        ServerOverloaded
    }

    public enum RequestedVideoQuality
    {
        [Description("[]")]
        Default = 0,
        [Description("240")]
        LD_240 = 1,
        [Description("480")]
        SD_480 = 2,
        [Description("720")]
        ED_720 = 3,
        [Description("1080")]
        HD_1080 = 4
    }

    public enum FallBackVideoQuality
    {
        Lowest = 0,
        Highest = 2,
    }
}
