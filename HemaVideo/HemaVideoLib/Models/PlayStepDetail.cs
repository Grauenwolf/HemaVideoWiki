namespace HemaVideoLib.Models
{
	public class PlayStepDetail
	{
		public char Actor { get; set; }
		public string ActorName { get; set; }
		public int? FootworkKey { get; set; }
		public string FootworkName { get; set; }
		public int? GuardKey { get; set; }
		public int? GuardModifierKey { get; set; }
		public string GuardModifierName { get; set; }
		public string GuardName { get; set; }
		public int? IntermediateGuardKey { get; set; }
		public int? IntermediateGuardModifierKey { get; set; }
		public string IntermediateGuardModifierName { get; set; }
		public string IntermediateGuardName { get; set; }
		public string Notes { get; set; }
		public int PlayKey { get; set; }
		public int? TargetKey1 { get; set; }
		public int? TargetKey2 { get; set; }
		public int? TargetKey3 { get; set; }
		public string TargetName1 { get; set; }
		public string TargetName2 { get; set; }
		public string TargetName3 { get; set; }
		public int? TechniqueKey1 { get; set; }
		public int? TechniqueKey2 { get; set; }
		public int? TechniqueKey3 { get; set; }
		public string TechniqueName1 { get; set; }
		public string TechniqueName2 { get; set; }
		public string TechniqueName3 { get; set; }
		public int TempoNumber { get; set; }
	}
}