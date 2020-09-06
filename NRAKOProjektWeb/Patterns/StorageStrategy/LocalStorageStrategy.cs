using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.StorageStrategy
{
    public class LocalStorageStrategy : IStorageStrategy
    {
        private readonly string _folderPath;

        public LocalStorageStrategy(string folderPath)
        {
            _folderPath = folderPath;
        }
        public string Store(MemoryStream ms, string fileName, out long fileSize)
        {
            string filePath = Path.Combine(_folderPath, fileName);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            ms.WriteTo(fs);
            string url = "~/uploads/" + fileName;
            fs.Close();
            FileInfo fi = new FileInfo(filePath);
            fileSize = fi.Length;
            return url;
        }
    }
}
