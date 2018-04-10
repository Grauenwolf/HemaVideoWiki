using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HemaVideoLib.Models
{
	public class SectionSummary
	{
		private int m_Depth;
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
		public int SectionKey { get; set; }
		public string SectionName { get; set; }
		public SectionSummaryCollection Subsections { get; } = new SectionSummaryCollection();
		public int TotalVideoCount => VideoCount + Subsections.Sum(x => x.TotalVideoCount);
		public int VideoCount { get; set; }

		public List<WeaponVersusSummary> Weapons { get; } = new List<WeaponVersusSummary>();

		/// <summary>
		/// Determines whether thsi section contains the specified weapon.
		/// </summary>
		/// <param name="weaponKey">The weapon key.</param>
		/// <param name="secondaryWeaponKey">The secondary weapon key. Optional.</param>
		/// <returns><c>true</c> if the weapon was found in this section or any of its subsections.</returns>
		public bool ContainsWeapon(int weaponKey, int? secondaryWeaponKey)
		{
			if (secondaryWeaponKey == null)
			{
				if (Weapons.Any(x => x.PrimaryWeaponKey == weaponKey))
					return true;
			}
			else
			{
				if (Weapons.Any(x => x.PrimaryWeaponKey == weaponKey && x.SecondaryWeaponKey == secondaryWeaponKey))
					return true;
			}
			return Subsections.Any(x => x.ContainsWeapon(weaponKey, secondaryWeaponKey));
		}
	}
}