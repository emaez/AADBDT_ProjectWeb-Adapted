using NRAKOProjektWeb.Patterns.MutationCommand;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns
{
    public class VigneteMutation : MutationBase, IImageMutation
    {
        public VigneteMutation(int amount) : base(amount)
        {
        }

        private void VigneteImage(Image<Rgba32> image)
        {
            _amount = (int)Math.Ceiling(_amount);
            image.Mutate(x => x.
            Vignette((image.Width / _amount*10), (image.Height / _amount*10))
            );
        }

        public override void Mutate(Image<Rgba32> image)
        {
            VigneteImage(image);
        }
    }
}
