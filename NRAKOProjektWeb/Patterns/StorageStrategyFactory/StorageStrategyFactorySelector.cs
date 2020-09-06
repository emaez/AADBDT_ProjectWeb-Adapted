using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NRAKOProjektWeb.Models;

namespace NRAKOProjektWeb.Patterns.StorageStrategyFactory
{
    public class StorageStrategyFactorySelector : IStorageStrategyFactorySelector
    {

        public IStorageStrategyFactory GetStorageStrategyFactory(StorageStrategySelectionData storageStrategySelectionData)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string factoryName = "NRAKOProjektWeb.Patterns.StorageStrategyFactory." + storageStrategySelectionData.StorageStrategyName + "StorageStrategyFactory";
            Type factoryType = assembly.GetType(factoryName);
            return (IStorageStrategyFactory)Activator.CreateInstance(factoryType, storageStrategySelectionData.Params);
        }
    }
}
