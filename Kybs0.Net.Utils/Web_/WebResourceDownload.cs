using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Net.Utils
{
    public class WebResourceDownload
    {
        /// <summary>
        /// 下载资源
        /// </summary>
        /// <param name="resourceUri"></param>
        /// <param name="downloadPath">本地路径</param>
        /// <returns></returns>
        public virtual bool Download(string resourceUri, string downloadPath)
        {
            if (string.IsNullOrWhiteSpace(resourceUri))
            {
                return false;
            }
            try
            {
                //"http://ydschool-online.nos.netease.com/account_v0205.mp3"
                WebRequest request = WebRequest.Create(resourceUri);
                WebResponse response = request.GetResponse();
                using (Stream reader = response.GetResponseStream())
                {
                    using (FileStream writer = new FileStream(downloadPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        byte[] buff = new byte[512];
                        int c = 0;                                           //实际读取的字节数   
                        while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, c);
                        }
                        response.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            //下载成功
            return true;
        }
    }
}
