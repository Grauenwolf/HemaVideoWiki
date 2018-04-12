using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class Play
	{
		public int? AGuardKey { get; set; }
		public int? AGuardModifierKey { get; set; }
		public int? MeasureKey { get; set; }
		public int? PGuardKey { get; set; }
		public int? PGuardModifierKey { get; set; }
		public int? PlayKey { get; set; }
		public int SectionKey { get; set; }
		public List<PlayStep> Steps => new List<PlayStep>();
		public string VariantName { get; set; }
	}
}