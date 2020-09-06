using NRAKOProjektWeb.Patterns.StorageStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public interface IStorageStrategyFactory
    {
        IStorageStrategy GetStorageStrategy();
    }
}
