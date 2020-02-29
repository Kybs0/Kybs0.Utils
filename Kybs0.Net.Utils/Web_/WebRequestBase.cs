using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Web;

namespace Kybs0.Net.Utils
{
    public class WebRequestBase
    {
        #region Request

        public virtual async Task<TReponse> RequestDataAsync<TReponse>(string requestUrl)
        {
            WebRequest translationWebRequest = WebRequest.Create(requestUrl);

            var response = await translationWebRequest.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException(),
                    Encoding.GetEncoding("utf-8")))
                {
                    string result = reader.ReadToEnd();
                    var decodeResult = UnicodeHelper.Unicode2String(result);
                    var dataResponse = JsonConvert.DeserializeObject<TReponse>(decodeResult);
                    return dataResponse;
                }
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// Post using WebRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<TReponse> PostDataAsync<TReponse>(string requestUrl, HttpRequest request)
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = "post";
            webRequest.ContentType = "application/json;charset=utf-8";

            var jsonData = JsonConvert.SerializeObject(request);
            byte[] postdatabyte = Encoding.UTF8.GetBytes(jsonData);
            webRequest.ContentLength = postdatabyte.Length;
            using (Stream postStream = webRequest.GetRequestStream())
            {
                postStream.Write(postdatabyte, 0, postdatabyte.Length);
            }

            var response = await webRequest.GetResponseAsync();

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream ?? throw new InvalidOperationException(),
                    Encoding.GetEncoding("utf-8")))
                {
                    string result = reader.ReadToEnd();
                    var decodeResult = UnicodeHelper.Unicode2String(result);
                    var dataResponse = JsonConvert.DeserializeObject<TReponse>(decodeResult);
                    return dataResponse;
                }
            }
        }
        /// <summary>
        /// Post using WebRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="request"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public virtual async Task<TReponse> PostDataAsync<TReponse>(string requestUrl, HttpRequest request, string userToken, string app_id, string app_key)
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = "post";
            webRequest.ContentType = "application/json";
            if (!string.IsNullOrEmpty(userToken))
            {
                webRequest.Headers.Add("Token", userToken);
                webRequest.Headers.Add("X‑C‑AppId", app_id);
                webRequest.Headers.Add("X-C-ApiKey", app_key);
            }

            var jsonData = JsonConvert.SerializeObject(request);
            byte[] postdatabyte = Encoding.UTF8.GetBytes(jsonData);
            webRequest.ContentLength = postdatabyte.Length;
            using (Stream postStream = webRequest.GetRequestStream())
            {
                postStream.Write(postdatabyte, 0, postdatabyte.Length);
            }

            var response = await webRequest.GetResponseAsync();

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream ?? throw new InvalidOperationException(),
                    Encoding.GetEncoding("utf-8")))
                {
                    string result = reader.ReadToEnd();
                    var decodeResult = UnicodeHelper.Unicode2String(result);
                    var dataResponse = JsonConvert.DeserializeObject<TReponse>(decodeResult);
                    return dataResponse;
                }
            }
        }

        /// <summary>
        /// Post using HttpClient
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<TReponse> PostDataUsingHttpAsync<TReponse>(string requestUrl, HttpRequest request, string userToken, string app_id, string app_key)
        {
            var jsonData = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(jsonData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!string.IsNullOrEmpty(userToken))
            {
                httpContent.Headers.Add("Token", userToken);
                httpContent.Headers.Add("X-C-AppId", app_id);
                httpContent.Headers.Add("X-C-ApiKey", app_key);
            }

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.PostAsync(requestUrl, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<TReponse>(result);
                return dataResponse;
            }
            return default(TReponse);
        }

        #endregion
    }
}
