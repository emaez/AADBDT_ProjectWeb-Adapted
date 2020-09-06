using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public interface IImageMutation
    {
        void Mutate(Image<Rgba32> image);   
    }
}
