using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CryptoCurrency.Core
{
    public class HttpProxy
    {
        public async static Task<string> Send(string url, HttpMethod method, NameValueCollection headers, string postData, string requestContentType = "application/json")
        {
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = method
                };

   