using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class BookDetail : BookSummary
	{
		public List<Author> Authors { get; } = new List<Author>();
		public SectionSummaryCollection Sections { get; } = new SectionSummaryCollection();
		public List<WeaponVersus> Weapons { get; } = new List<WeaponVersus>();

		public BookStats Stats { get; set; }

		public bool CanEdit { get; set; }
	}
}