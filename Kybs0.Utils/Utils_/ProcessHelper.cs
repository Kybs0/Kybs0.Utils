using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Net.Utils
{
    public static class ProcessHelper
    {
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName)
        {
            try
            {
                //删除所有同名进程
                Process currentProcess = Process.GetCurrentProcess();
                var processes = Process.GetProcessesByName(processName).Where(process => process.Id != currentProcess.Id);
                foreach (Process thisproc in processes)
                {
                    thisproc.Kill();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
