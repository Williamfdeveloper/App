using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Adapter
{
    public class ApiBase
    {
        HttpClient client = new HttpClient();

        #region API

        public HttpStatusCode Request<TBody, TResult>(TBody body, IDictionary<string, string> Headers, IDictionary<string, string> @params, string Url, HttpMethod Method, string BearerToken, out TResult Response, out string message)
        where TResult : new()
        {
            string urlParam = string.Empty;
            if (@params != null)
                foreach (var p in @params)
                {
                    urlParam += string.IsNullOrEmpty(urlParam) ? $"?{p.Key}={p.Value}" : $"&{p.Key}={p.Value}";
                }

            var request = new HttpRequestMessage
            {
                Method = Method,
                RequestUri = new Uri(string.Concat(Url, urlParam)) /*new Uri(Url),*/
            };

            if (Headers != null)
                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

            if (!string.IsNullOrEmpty(BearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            var responseMessage = RequisicaoAsync(request).Result;
            if (responseMessage != null)
            {
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    Response = JsonConvert.DeserializeObject<TResult>(ResponseAsync(responseMessage).Result.ToString());
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        message = string.Empty;
                        return responseMessage.StatusCode;
                    }
                    else
                    {
                        message = responseMessage.ToString();
                        return responseMessage.StatusCode;
                    }
                }
                else
                {
                    Response = new TResult();
                    message = responseMessage.ToString();
                    return responseMessage.StatusCode;
                }

            }
            else
            {
                Response = new TResult();
                message = responseMessage.ToString();
                return responseMessage.StatusCode;
            }

        }

        public async Task<HttpResponseMessage> RequisicaoAsync(HttpRequestMessage request)
        {
            return await client.SendAsync(request);
        }

        public async Task<object> ResponseAsync(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadAsStringAsync();
        }

        #endregion

    }
}
