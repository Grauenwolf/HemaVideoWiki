using System;
using System.Linq;

namespace Extractor
{
    class Video
    {
        public int VideoKey { get; set; }
        public int SectionKey { get; set; }
        public string VideoServiceVideoId { get; set; }
        public string Url { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int CreatedByUserKey { get; set; } = 1;
        public string VideoServiceName { get; set; }
        public int VideoServiceKey { get; set; }



    }
}
