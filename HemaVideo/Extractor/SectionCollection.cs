using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Extractor
{
    class SectionCollection : Collection<Section>
    {
        public SectionCollection(int bookKey)
        {
            SectionKeyOffset = 1000 * (bookKey);
        }
        public int SectionKeyOffset { get; }

        public int NextSectionKey => SectionKeyOffset + Count + 1;
    }
}
