using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Net.Utils
{
    public static class UtilsCommonPath
    {
        public static string GetLogPath()
        {
            string appdataPath = GetAppDataFolder();
            string logFilePath = Path.Combine(appdataPath, "log.txt");

            if (!File.Exists(logFilePath))
            {
                var aaa = File.Create(logFilePath);
                aaa.Dispose();
            }

            return logFilePath;
        }

        private static string _appDataFolder = string.Empty;
        public static string GetAppDataFolder()
        {
            if (string.IsNullOrEmpty(_appDataFolder))
            {
                Process cur = Process.GetCurrentProcess();
                _appDataFolder = Path.Combine(@"C:\Users\" + Environment.UserName + $"\\AppData\\Roaming\\{cur.ProcessName}");
            }

            if (!Directory.Exists(_appDataFolder))
            {
                Directory.CreateDirectory(_appDataFolder);
            }
            return _appDataFolder;
        }


        public static string ExeRunFolder()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return baseDirectory;
        }
    }
}
