using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace NRAKOProjektWeb.Patterns.MutationCommand
{
    public class ResizeMutation : IImageMutation
    {
        private int _width;
        private int _height;
        public ResizeMutation(int width, int height)
        {
            _width = width;
            _height = height;
        }

        private void ResizeImage(Image<Rgba32> image) {
            image.Mutate(x => x
            .Resize(_width, _height)
            );
        }

        public void Mutate(Image<Rgba32> image)
        {
            ResizeImage(image);
        }
    }
}
