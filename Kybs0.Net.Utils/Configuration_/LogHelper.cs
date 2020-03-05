using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var infos=new List<string>()
                {
                    $"记录时间:{DateTime.Now:yyyy-MM-dd hh:mm:ss}",
                    $"描述:{message}\r\n"
                };
                string logFilePath = GetLogInfoPath();
                File.AppendAllLines(logFilePath, infos);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        public static void LogError(string message)
        {
            try
            {
                var infos = new List<string>()
                {
                    $"记录时间:{DateTime.Now.ToLongTimeString()}",
                    $"异常描述:{message}",
                };
                string logFilePath = GetLogErrorPath();
                File.AppendAllLines(logFilePath, infos);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        public static void LogError(string message,Exception ex)
        {
            try
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
            catch (Exception e)
            {
                // ignored
            }
        }
        public static void LogError(Exception ex)
        {
            try
            {
                var infos = new List<string>()
                {
                    $"记录时间:{DateTime.Now.ToLongTimeString()}",
                    $"异常详细:{ex.Message}\r\n{ex.StackTrace}\r\n",
                };
                string logFilePath = GetLogErrorPath();
                File.AppendAllLines(logFilePath, infos);
            }
            catch (Exception e)
            {
                // ignored
            }
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

            try
            {
                if (!File.Exists(logFilePath))
                {
                    var aaa = File.Create(logFilePath);
                    aaa.Dispose();
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            return logFilePath;
        }
    }
}
