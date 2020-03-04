using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Net.Utils
{
    public static class FileHelper
    {
        public static void MoveFiles(string sourceFolder, string destFolder)
        {
            if (Directory.Exists(sourceFolder) && Directory.Exists(destFolder))
            {
                foreach (var x in Directory.EnumerateFiles(sourceFolder, "*.*", SearchOption.TopDirectoryOnly))
                {
                    var fileName = Path.GetFileName(x);
                    var newFile = Path.Combine(destFolder, fileName ?? throw new InvalidOperationException());
                    if (File.Exists(newFile))
                    {
                        File.Delete(newFile);
                    }
                    File.Move(x, newFile);
                }
            }
        }
        public static void RemoveFiles(string dir)
        {
            foreach (var x in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories))
            {
                File.Delete(x);
            }
        }
        public static void RemoveFiles(string dir, string pattern)
        {
            foreach (var x in Directory.EnumerateFiles(dir, pattern, SearchOption.AllDirectories))
            {
                File.Delete(x);
            }
        }
        public static string CopyTempFile(string file)
        {
            var tempPptPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(file));
            File.Copy(file, tempPptPath);
            return tempPptPath;
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
