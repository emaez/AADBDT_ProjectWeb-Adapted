using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Patterns.StorageStrategy;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public class AmazonS3StorageStrategyFactory : StorageStrategyFactoryBase, IStorageStrategyFactory
    {
        public AmazonS3StorageStrategyFactory(Dictionary<string, string> storageParams) : base(storageParams)
        {
        }

        public override IStorageStrategy GetStorageStrategy()
        {
            return new AmazonS3StorageStrategy();
        }
    }
}
