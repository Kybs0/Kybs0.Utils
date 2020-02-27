using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Kybs0.Net.Utils
{
    public class WebRequestBase
    {
        public virtual async Task<string> RequestDataAsync(string requestUrl)
        {
            WebRequest translationWebRequest = WebRequest.Create(requestUrl);

            var response = await translationWebRequest.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException(),
                    Encoding.GetEncoding("utf-8")))
                {
                    string result = reader.ReadToEnd();
                    var decodeResult = Unicode2String(result);
                    return decodeResult;
                }
            }
        }
        /// <summary>
        /// Post using WebRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public virtual async Task<string> PostDataAsync(string requestUrl, string jsonData)
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = "post";
            webRequest.ContentType = "application/json;charset=utf-8";

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
                    var decodeResult = Unicode2String(result);
                    return decodeResult;
                }
            }
        }
        /// <summary>
        /// Post using WebRequest
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="jsonData"></param>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public virtual async Task<string> PostDataAsync(string requestUrl, string jsonData, string userToken, string app_id, string app_key)
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
                    var decodeResult = Unicode2String(result);
                    return decodeResult;
                }
            }
        }

        /// <summary>
        /// Post using HttpClient
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public virtual async Task<string> PostDataUsingHttpAsync(string requestUrl, string jsonData, string userToken, string app_id, string app_key)
        {
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
                Task<string> t = response.Content.ReadAsStringAsync();
                return t.Result;
            }
            return string.Empty;
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        protected virtual string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}
