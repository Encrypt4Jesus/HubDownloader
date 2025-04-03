using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubDownloader
{
    public delegate void NewResultsAvailableEventHandler(object? sender, NewResultsAvailableEventArgs e);

    public class NewResultsAvailableEventArgs : EventArgs
    {
        public static readonly NewResultsAvailableEventArgs Empty = new NewResultsAvailableEventArgs();

        public UrlExtractionResultObject Results { get; private set; }

        private NewResultsAvailableEventArgs()
            : base()
        {
            Results = UrlExtractionResultObject.Empty;
        }

        public NewResultsAvailableEventArgs(UrlExtractionResultObject results)
            : base()
        {
            Results = results;
        }
    }
}
