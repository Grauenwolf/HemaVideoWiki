using System;
using System.Collections.Generic;

namespace HemaVideoLib
{
    public class SectionSummary
    {
        public int SectionKey { get; set; }
        public int BookKey { get; set; }
        public int? ParentSectionKey { get; set; }
        public string SectionName { get; set; }
        public string PageReference { get; set; }

        public List<SectionSummary> Subsections { get; } = new List<SectionSummary>();

    }
}


