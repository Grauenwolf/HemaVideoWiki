﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tortuga.Anchor;

namespace HemaVideoLib.Models
{
	public class SectionSummaryCollection : Collection<SectionSummary>
	{
		public bool IsFilteredByWeapon { get; set; }

		public void FilterSectionsByWeapon(int weaponKey, int? secondaryWeaponKey)
		{
			if (Count == 0)
				return;

			//fitler the current list
			var newList = new List<SectionSummary>();
			foreach (var section in this)
			{
				if (section.ContainsWeapon(weaponKey, secondaryWeaponKey))
					newList.Add(section);
			}
			this.Clear();
			this.AddRange(newList);

			//filter the child lists
			foreach (var section in this)
			{
				section.Subsections.FilterSectionsByWeapon(weaponKey, secondaryWeaponKey);
			}

			IsFilteredByWeapon = true;
		}

		public IEnumerable<PlaySummary> ChildPlays()
		{
			foreach (var section in this)
			{
				foreach (var play in section.Plays)
					yield return play;

				foreach (var play in section.Subsections.ChildPlays())
					yield return play;
			}
		}
	}
}