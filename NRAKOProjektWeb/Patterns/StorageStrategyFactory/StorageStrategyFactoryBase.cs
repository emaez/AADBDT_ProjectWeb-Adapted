using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Patterns.StorageStrategy;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public abstract class StorageStrategyFactoryBase : IStorageStrategyFactory
    {
        protected Dictionary<string, string> _storageParams;
        public StorageStrategyFactoryBase(Dictionary<string, string> storageParams)
        {
            _storageParams = storageParams;
        }

        public virtual IStorageStrategy GetStorageStrategy()
        {
            throw new NotImplementedException();
        }
    }
}
