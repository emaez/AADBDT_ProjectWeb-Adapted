using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public class FormatMutation : IImageMutation
    {
        public void Mutate(Image<Rgba32> image)
        {
        }
    }
}
