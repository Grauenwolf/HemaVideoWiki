namespace HemaVideoLib.Models
{
	public class PlayStep
	{
		public char Actor { get; set; }
		public int? FootworkKey { get; set; }
		public int? GuardKey { get; set; }
		public int? GuardModifierKey { get; set; }
		public int? IntermediateGuardKey { get; set; }
		public int? IntermediateGuardModifierKey { get; set; }
		public string Notes { get; set; }
		public int? PlayKey { get; set; }
		public int? Target1 { get; set; }
		public int? Target2 { get; set; }
		public int? Target3 { get; set; }
		public int? TechniqueKey1 { get; set; }
		public int? TechniqueKey2 { get; set; }
		public int? TechniqueKey3 { get; set; }
		public int TempoNumber { get; set; }
	}
}