using NRAKOProjektWeb.Patterns.MutationCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public class ResizeMutationFactory : MutationActionFactoryBase, IMutationActionFactory
    {
        public ResizeMutationFactory(Dictionary<string, string> mutationParams) : base(mutationParams)
        {
        }

        public override IImageMutation GetMutationAction()
        {
            int width = 1;
            int.TryParse(_mutationParams["resizeWidth"], out width);
            int height = 1;
            int.TryParse(_mutationParams["resizeHeight"], out height);

            return new ResizeMutation(width,height);
        }
    }
}
