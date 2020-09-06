using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public class SepiaMutation : MutationBase, IImageMutation
    {
        public SepiaMutation(int amount) : base(amount)
        {
        }

        private void SepiaImage(Image<Rgba32> image)
        {
            image.Mutate(x => x.
                Sepia(_amount)
            );
        }

        override public void Mutate(Image<Rgba32> image)
        {
            SepiaImage(image);
        }
    }
}
