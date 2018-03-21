using System;
using System.Collections.Generic;
using System.Linq;

namespace Extractor
{
    class Section
    {
        public int BookKey { get; set; }
        public int SectionKey { get; set; }
        public int? ParentSectionKey { get; set; }
        public string SectionName { get; set; }

        public string PageReference { get; set; }
        public string FileName { get; set; }
        public int DisplayOrder { get; set; }
        public List<Video> Videos { get; } = new List<Video>();
    }
}
