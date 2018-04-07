using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class BookDetail : BookSummary
	{
		public List<Author> Authors { get; } = new List<Author>();
		public List<SectionSummary> Sections { get; } = new List<SectionSummary>();
		public List<Weapon> Weapons { get; } = new List<Weapon>();
	}
}