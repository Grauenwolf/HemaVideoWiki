﻿namespace HemaVideoLib.Models
{
	public class WeaponVersus
	{
		public int SectionKey { get; set; }
		public int PrimaryWeaponKey { get; set; }
		public string PrimaryWeaponName { get; set; }
		public int? SecondaryWeaponKey { get; set; }
		public string SecondaryWeaponName { get; set; }
	}
}