namespace HemaVideoLib.Models
{
	public class WeaponVersus
	{
		public int PrimaryWeaponKey { get; set; }
		public string PrimaryWeaponName { get; set; }
		public int? SecondaryWeaponKey { get; set; }
		public string SecondaryWeaponName { get; set; }
	}

	public class WeaponVersusSummary
	{
		public int SectionKey { get; set; }
		public int PrimaryWeaponKey { get; set; }
		public int? SecondaryWeaponKey { get; set; }
	}
}