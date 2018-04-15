using HemaVideoTools.Services;
using System.Collections.Generic;
using System.Linq;

namespace HemaVideoTools
{
	public class Tags
	{
		public Tags(IEnumerable<Footwork> footwork,
			IEnumerable<Technique> techniques,
			IEnumerable<Target> targets,
			IEnumerable<Guard> guards,
			IEnumerable<GuardModifier> guardModifiers,
			IEnumerable<Measure> measures)
		{
			Footwork = footwork.ToList();
			Techniques = techniques.ToList();
			Targets = targets.ToList();
			Guards = guards.ToList();
			GuardModifiers = guardModifiers.ToList();
			Measures = measures.ToList();

			Footwork.Insert(0, new Footwork() { });
			Techniques.Insert(0, new Technique() { });
			Targets.Insert(0, new Target() { });
			Guards.Insert(0, new Guard() { });
			GuardModifiers.Insert(0, new GuardModifier() { });
			Measures.Insert(0, new Measure() { });
		}

		public List<Footwork> Footwork { get; }
		public List<Technique> Techniques { get; }
		public List<Target> Targets { get; }
		public List<Guard> Guards { get; }
		public List<GuardModifier> GuardModifiers { get; }
		public List<Measure> Measures { get; }
	}
}