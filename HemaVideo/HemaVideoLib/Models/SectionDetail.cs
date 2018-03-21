using System;
using System.Collections.Generic;

namespace HemaVideoLib.Models
{
    public class SectionDetail : SectionSummary
    {
        public List<Video> Videos { get; } = new List<Video>();
    }
}


