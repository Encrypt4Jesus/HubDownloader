using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public class UrlExtractionResultObject
    {
        public static readonly UrlExtractionResultObject Empty = new UrlExtractionResultObject();

        private UrlExtractionResultObject()
        { }

        public UrlExtractionResultObject(string name, string url)
            : this()
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
