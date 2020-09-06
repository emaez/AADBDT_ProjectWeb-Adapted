using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.MutationCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public interface IMutationActionFactory
    {
        IImageMutation GetMutationAction();
    }
}
