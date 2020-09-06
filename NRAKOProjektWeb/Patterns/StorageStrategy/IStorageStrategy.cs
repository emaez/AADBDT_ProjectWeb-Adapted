using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.StorageStrategy
{
    public interface IStorageStrategy
    {
        string Store(MemoryStream ms, string fileName, out long fileSize);
    }
}
