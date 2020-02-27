using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Net.Utils
{
    public static class LogHelper
    {
        public static void LogInfo(string message)
        {
            var infos=new List<string>()
            {
                $"记录时间:{DateTime.Now.ToLongTimeString()}",
                $"描述:{message}\r\n"
            };
            string logFilePath = GetLogInfoPath();
            File.AppendAllLines(logFilePath, infos);
        }

        public static void LogError(string message,Exception ex)
        {
            var infos = new List<string>()
            {
                $"记录时间:{DateTime.Now.ToLongTimeString()}",
                $"异常描述:{message}",
                $"异常详细:{ex.Message}\r\n{ex.StackTrace}\r\n",
            };
            string logFilePath = GetLogErrorPath();
            File.AppendAllLines(logFilePath, infos);
        }
        public static void LogError(Exception ex)
        {
            var infos = new List<string>()
            {
                $"记录时间:{DateTime.Now.ToLongTimeString()}",
                $"异常详细:{ex.Message}\r\n{ex.StackTrace}\r\n",
            };
            string logFilePath = GetLogErrorPath();
            File.AppendAllLines(logFilePath, infos);
        }

        private static string GetLogInfoPath()
        {
            return GetLogInfoFilePath("info.txt");
        }
        private static string GetLogErrorPath()
        {
            return GetLogInfoFilePath("error.txt");
        }
        private static string GetLogInfoFilePath(string fileName)
        {
            string logFolder = UtilsCommonPath.GetLogFolder();
            string logFilePath = Path.Combine(logFolder, fileName);

            if (!File.Exists(logFilePath))
            {
                var aaa = File.Create(logFilePath);
                aaa.Dispose();
            }

            return logFilePath;
        }
    }
}
