using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.MutationCommand;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public class BlurMutationFactory : MutationActionFactoryBase, IMutationActionFactory
    {
        public BlurMutationFactory(Dictionary<string, string> mutationParams) : base(mutationParams)
        {
        }

        public override IImageMutation GetMutationAction()
        {
            int amount = 1;
            int.TryParse(_mutationParams["amount"], out amount);

            return new BlurMutation(amount);
        }
    }
}
