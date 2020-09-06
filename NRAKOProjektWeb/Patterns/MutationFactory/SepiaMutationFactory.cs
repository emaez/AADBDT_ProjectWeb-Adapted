using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Patterns.MutationCommand;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public class SepiaMutationFactory : MutationActionFactoryBase, IMutationActionFactory
    {
        public SepiaMutationFactory(Dictionary<string, string> mutationParams) : base(mutationParams)
        {
        }

        public override IImageMutation GetMutationAction()
        {
            int amount = 1;
            int.TryParse(_mutationParams["amount"], out amount);

            return new SepiaMutation(amount);
        }
    }
}
