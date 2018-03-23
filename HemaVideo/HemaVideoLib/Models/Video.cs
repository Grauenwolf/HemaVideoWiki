using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HemaVideoLib.Models
{
    public class Video
    {
        public int CreatedByUserKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SectionKey { get; set; }
        public TimeSpan? StartTime { get; set; }
        public string Url { get; set; }
        public int VideoKey { get; set; }
        public int VideoServiceKey { get; set; }
        public string VideoServiceVideoId { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
    }
}


