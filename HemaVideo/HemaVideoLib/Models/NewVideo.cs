using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HemaVideoLib.Models
{
    [Table("Interpretations.Video")]
    [DataContract]
    public class NewVideo
    {
        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int SectionKey { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public string Url { get; set; }

        public int VideoServiceKey { get; set; }
        public string VideoServiceVideoId { get; set; }
    }
}


