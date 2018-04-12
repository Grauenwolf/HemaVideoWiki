using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class PlayDetail
	{
		public int? AGuardKey { get; set; }
		public int? AGuardModifierKey { get; set; }
		public string AGuardModifierName { get; set; }
		public string AGuardName { get; set; }
		public int? MeasureKey { get; set; }
		public string MeasureName { get; set; }
		public int? PGuardKey { get; set; }
		public int? PGuardModifierKey { get; set; }
		public string PGuardModifierName { get; set; }
		public string PGuardName { get; set; }
		public int PlayKey { get; set; }
		public int SectionKey { get; set; }
		public List<PlayStepDetail> Steps => new List<PlayStepDetail>();
		public string VariantName { get; set; }
	}
}