using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class SectionDetail
	{
		public int BookKey { get; set; }
		public string BookName { get; set; }
		public string PageReference { get; set; }
		public int? ParentSectionKey { get; set; }
		public int SectionKey { get; set; }
		public string SectionName { get; set; }
		public SectionSummaryCollection Subsections { get; } = new SectionSummaryCollection();
		public int VideoCount { get; set; }
		public List<Video> Videos { get; } = new List<Video>();
		public List<WeaponVersus> Weapons { get; } = new List<WeaponVersus>();

		public List<PlayDetail> Plays { get; } = new List<PlayDetail>();
	}
}