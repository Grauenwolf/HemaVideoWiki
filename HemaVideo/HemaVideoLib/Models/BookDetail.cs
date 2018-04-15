using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class BookDetail : BookSummary
	{
		public List<Author> Authors { get; } = new List<Author>();
		public SectionSummaryCollection Sections { get; } = new SectionSummaryCollection();
		public List<WeaponVersus> Weapons { get; } = new List<WeaponVersus>();

		public BookStats Stats { get; set; }
	}

	public class BookStats
	{
		public List<Tag> Techniques { get; set; }
		public List<Tag> Guards { get; set; }
		public List<Tag> Footwork { get; set; }
		public List<Tag> Targets { get; set; }
	}
}