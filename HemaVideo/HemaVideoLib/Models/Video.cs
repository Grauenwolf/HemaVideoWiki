using System;

namespace HemaVideoLib.Models
{
    public class Video
    {
        public int VideoKey { get; set; }
        public int SectionKey { get; set; }
        public string YouTubeId { get; set; }
        public string Url { get; set; }

        public TimeSpan? StartTime { get; set; }
        public int CreatedByUserKey { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}


