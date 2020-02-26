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
        public static void LogInfo(string text)
        {
            LogInfo(new List<string>()
            {
                text
            });
        }

        public static void LogInfo(List<string> lines)
        {
            string logFilePath = UtilsCommonPath.GetLogPath();
            File.AppendAllLines(logFilePath, lines);
        }

        public static void LogError(Exception ex)
        {
            LogInfo(ex.Message);
        }
    }
}
