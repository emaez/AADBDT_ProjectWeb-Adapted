using NRAKOProjektWeb.Patterns.Facade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.StorageStrategy
{
    public class AmazonS3StorageStrategy : IStorageStrategy
    {
        private readonly AmazonS3Tools _amazonS3Tools;

        public AmazonS3StorageStrategy()
        {
            _amazonS3Tools = new AmazonS3Tools();
        }

        public string Store(MemoryStream ms, string fileName, out long fileSize)
        {
            fileSize = _amazonS3Tools.UploadToS3(ms, fileName);
            return "https://s3.eu-central-1.amazonaws.com/nrakoprojekt/" + fileName;
        }
    }
}
