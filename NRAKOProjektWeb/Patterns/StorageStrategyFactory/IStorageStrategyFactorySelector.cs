using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public interface IStorageStrategyFactorySelector
    {
        IStorageStrategyFactory GetStorageStrategyFactory(StorageStrategySelectionData storageStrategySelectionData);
    }
}
