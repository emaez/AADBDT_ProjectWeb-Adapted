using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public abstract class MutationBase : IImageMutation
    {
        protected float _amount;

        public MutationBase(int amount)
        {
            _amount = (float)(0.1 * amount);

        }

        public virtual void Mutate(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }
    }
}
