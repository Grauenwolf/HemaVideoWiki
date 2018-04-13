using System.Collections.Generic;
using System.Linq;

namespace HemaVideoLib.Models
{
	public class PlayDetail
	{
		public string AAlternateGuardName { get; set; }
		public string AGuardFullName => Formatter.MultiPart(AGuardName, AAlternateGuardName, AGuardModifierName);
		public int? AGuardKey { get; set; }
		public int? AGuardModifierKey { get; set; }
		public string AGuardModifierName { get; set; }
		public string AGuardName { get; set; }
		public int? MeasureKey { get; set; }
		public string MeasureName { get; set; }
		public string PAlternateGuardName { get; set; }
		public string PGuardFullName => Formatter.MultiPart(PGuardName, PAlternateGuardName, PGuardModifierName);
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