using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface IHttpService
    {
        void GetResponse<T>(Uri uri, Action<T> callback, ISerialize serializer) where T : class;

        string GetResponse(string url);

    }

}
