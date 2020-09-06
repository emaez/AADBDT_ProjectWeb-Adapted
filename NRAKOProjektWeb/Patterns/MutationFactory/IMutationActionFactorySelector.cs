using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public interface IMutationActionFactorySelector
    {
        IMutationActionFactory GetMutationActionFactory(MutationAction mutationAction);
    }
}
