using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NRAKOProjektWeb.Patterns.MutationCommand;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NRAKOProjektWeb.Patterns.MutationFactory
{
    public abstract class MutationActionFactoryBase : IMutationActionFactory
    {
        protected Dictionary<string, string> _mutationParams;
        public MutationActionFactoryBase( Dictionary<string, string> mutationParams)
        {
            _mutationParams = mutationParams;
        }
        public virtual IImageMutation GetMutationAction()
        {
            throw new NotImplementedException();
        }
    }
}
