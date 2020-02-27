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
        public static string GetLogFolder()
        {
            string appdataPath = GetAppDataFolder();
            string logFilePath = Path.Combine(appdataPath, "Log");

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
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
