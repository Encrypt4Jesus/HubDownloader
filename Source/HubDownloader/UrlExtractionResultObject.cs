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
        public string Name { get; set; }
        public string Url { get; set; }
        public string ViewKey { get; set; }
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

        public UrlExtractionResultObject(string viewKey)
        {
            ViewKey = viewKey;
        }

        public UrlExtractionResultObject(string name, string url)
        {
            Name = name;
            Url = url;
            IsSuccess = true;
            _errorReason = ExtractionWorkflowResult.None;
        }
    }
}
