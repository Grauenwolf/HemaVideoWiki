using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HemaVideoLib.Models
{
	public class SectionSummary
	{
		int m_Depth;
		public int BookKey { get; set; }

		[NotMapped]
		public int Depth
		{
			get => m_Depth;
			set
			{
				m_Depth = value;
				foreach (var section in Subsections)
					section.Depth = m_Depth + 1;
			}
		}

		public string PageReference { get; set; }
		public int? ParentSectionKey { get; set; }
		public int? PrimaryWeaponKey { get; set; }
		public string PrimaryWeaponName { get; set; }
		public int? SecondaryWeaponKey { get; set; }
		public string SecondaryWeaponName { get; set; }
		public int SectionKey { get; set; }
		public string SectionName { get; set; }
		public List<SectionSummary> Subsections { get; } = new List<SectionSummary>();
		public int TotalVideoCount => VideoCount + Subsections.Sum(x => x.TotalVideoCount);
		public int VideoCount { get; set; }
	}
}