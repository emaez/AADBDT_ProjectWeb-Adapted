using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class FilterDataViewModel
    {
        public int MaxSize { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int SizeRangeMin { get; set; }
        public int SizeRangeMax { get; set; }
        public int WidthRangeMin { get; set; }
        public int WidthRangeMax { get; set; }
        public int HeightRangeMin { get; set; }
        public int HeightRangeMax { get; set; }
        public string HashtagsFilter { get; set; }
        public string AuthorFilter { get; set; }

    }
}
