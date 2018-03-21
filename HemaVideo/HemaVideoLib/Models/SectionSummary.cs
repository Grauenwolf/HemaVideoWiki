using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemaVideoLib.Models
{
    public class SectionSummary
    {
        int m_Depth;
        public int BookKey { get; set; }

        [NotMapped]
        public int Depth
        {
            get => m_Depth;
            set
            {
                m_Depth = value;
                foreach (var section in Subsections)
                    section.Depth = m_Depth + 1;
            }
        }

        public string PageReference { get; set; }
        public int? ParentSectionKey { get; set; }
        public int SectionKey { get; set; }
        public string SectionName { get; set; }
        public List<SectionSummary> Subsections { get; } = new List<SectionSummary>();
        public int VideoCount { get; set; }
    }
}


