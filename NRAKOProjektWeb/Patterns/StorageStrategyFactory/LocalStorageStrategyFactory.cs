using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Patterns.StorageStrategy;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public class LocalStorageStrategyFactory : StorageStrategyFactoryBase, IStorageStrategyFactory
    {
        public LocalStorageStrategyFactory(Dictionary<string, string> storageParams) : base(storageParams)
        {
        }

        public override IStorageStrategy GetStorageStrategy()
        {
            return new LocalStorageStrategy(_storageParams["folderPath"]);
        }
    }
}
