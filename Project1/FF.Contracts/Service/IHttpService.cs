using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface IHttpService
    {
        void GetResponse<T>(Uri uri, Action<T> callback) where T : class;

        void GetPostResponse<T>(Uri uri, string data, Action<T> callback) where T : class;

        string GetResponse(string url);

    }

}
