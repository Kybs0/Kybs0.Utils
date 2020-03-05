using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                var canGetAppName=TryGetAppName(out var appName);
                if (!canGetAppName)
                {
                    var baseDirectory = Directory.GetCurrentDirectory();
                    appName = new DirectoryInfo(baseDirectory).Name;
                }
                _appDataFolder = Path.Combine(@"C:\Users\" + Environment.UserName + $"\\AppData\\Roaming\\{appName}");
            }

            if (!Directory.Exists(_appDataFolder))
            {
                Directory.CreateDirectory(_appDataFolder);
            }
            return _appDataFolder;
        }

        private static bool TryGetAppName(out string appName)
        {
            appName = string.Empty;
            try
            {
                var baseDirectory = Directory.GetCurrentDirectory();
                string startupUri = Application.Current.StartupUri.AbsolutePath;
                appName = Path.GetFileNameWithoutExtension(startupUri);
                if (string.IsNullOrEmpty(appName))
                {
                    var exeFile = Directory.GetFiles(baseDirectory, "*.exe").FirstOrDefault();
                    if (!string.IsNullOrEmpty(exeFile))
                    {
                        appName = Path.GetFileNameWithoutExtension(exeFile);
                    }
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            return !string.IsNullOrEmpty(appName);
        }

        public static void SetAppDataFolder(string appName)
        {
            _appDataFolder = Path.Combine(@"C:\Users\" + Environment.UserName + $"\\AppData\\Roaming\\{appName}");
            if (!Directory.Exists(_appDataFolder))
            {
                Directory.CreateDirectory(_appDataFolder);
            }
        }

        public static string GetExeRunFolder()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return baseDirectory;
        }

        public static string EnsureDirectory(string folder)
        {
            FolderHelper.CreateFolder(folder);
            return folder;
        }
    }
}
