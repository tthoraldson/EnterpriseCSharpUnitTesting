using FF.Contracts.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace FF.Service
{
    public class HttpService : IHttpService
    {
        private IWebClient _webClient;
        public HttpService(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public string GetResponse(string url)
        {
            return _webClient.DownloadString(url);
        }

        public void GetResponse<T>(Uri uri, Action<T> callback) where T : class
        {
            _webClient.OpenReadCompleted += (o, a) =>
            {
                if (callback != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    callback(ser.ReadObject(a.Result) as T);
                }
            };
            _webClient.OpenReadAsync(uri);
        }

        public void GetPostResponse<T>(Uri uri, string data, Action<T> callback) where T : class
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "text/plain;charset=utf-8";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(data);

            request.ContentLength = bytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
            }

            request.BeginGetResponse((x) =>
            {
                using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(x))
                {
                    if (callback != null)
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                        callback(ser.ReadObject(response.GetResponseStream()) as T);
                    }
                }
            }, null);
        }
    }
}
