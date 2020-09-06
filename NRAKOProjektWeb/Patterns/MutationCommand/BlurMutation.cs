using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public class BlurMutation : MutationBase, IImageMutation
    {
        public BlurMutation(int amount) : base(amount)
        {
        }

        private void BlurImage(Image<Rgba32> image) {
            image.Mutate(x => x.
            GaussianBlur(_amount)
            );
        }

        public override void Mutate(Image<Rgba32> image)
        {
            BlurImage(image);
        }

    }
}
