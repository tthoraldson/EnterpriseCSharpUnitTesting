using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface IWebClient
    {
        string DownloadString(string url);

        event OpenReadCompletedEventHandler OpenReadCompleted;

        void OpenReadAsync(Uri address);

    }
}
