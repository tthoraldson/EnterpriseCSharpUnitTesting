using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FF.Contracts.Service;

namespace FF.Service
{
    public class WebClient : IWebClient
    {
        public string DownloadString(string url)
        {
            throw new NotImplementedException();
        }

        public event OpenReadCompletedEventHandler OpenReadCompleted;
        public void OpenReadAsync(Uri address)
        {
            throw new NotImplementedException();
        }
    }
}
