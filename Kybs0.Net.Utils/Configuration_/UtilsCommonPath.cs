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
        public static string GetDownloadFolder()
        {
            var downloadFolder = Path.Combine(GetAppDataFolder(), "Download");
            downloadFolder = EnsureDirectory(downloadFolder);
            return downloadFolder;
        }
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
                var friendlyName = AppDomain.CurrentDomain.FriendlyName;
                friendlyName = friendlyName.Replace(".exe",string.Empty);
                _appDataFolder = Path.Combine(@"C:\Users\" + Environment.UserName + $"\\AppData\\Roaming\\{friendlyName}");
            }

            if (!Directory.Exists(_appDataFolder))
            {
                Directory.CreateDirectory(_appDataFolder);
            }
            return _appDataFolder;
        }


        public static string GetExeRunFolder()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return baseDirectory;
        }

        public static string EnsureDirectory(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }
    }
}
