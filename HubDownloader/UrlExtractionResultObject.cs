using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public class UrlExtractionResultObject
    {
        public static readonly UrlExtractionResultObject Empty = new UrlExtractionResultObject();

        public UrlExtractionResultObject()
        { }

        public UrlExtractionResultObject(string name, string url)
            : this()
        {
            Name = name;
            Url = url;
            IsSuccess = true;
            _errorReason = ExtractionWorkflowResult.None;
        }

        public bool IsSuccess { get; set; }
        public StringBuilder DebugInformation { get; set; }
        public ExtractionWorkflowResult ErrorReason
        {
            get { return _errorReason; }
            set
            {
                _errorReason = value;
                if (_errorReason != ExtractionWorkflowResult.None)
                {
                    IsSuccess = false;
                }
            }
        }
        private ExtractionWorkflowResult _errorReason;

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
