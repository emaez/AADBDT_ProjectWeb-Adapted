using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NRAKOProjektWeb.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public class MutationActionFactorySelector : IMutationActionFactorySelector
    {
        public IMutationActionFactory GetMutationActionFactory(MutationAction mutationAction)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string factoryName = "NRAKOProjektWeb.Patterns.MutationFactory." + mutationAction.Action + "MutationFactory";
            Type factoryType = assembly.GetType(factoryName);
            return (IMutationActionFactory)Activator.CreateInstance(factoryType, mutationAction.Params);
        }

    }
}
