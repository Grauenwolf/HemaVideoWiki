using HemaVideoTools.Services;
using System.Collections.ObjectModel;

namespace HemaVideoTools
{
	public class Tags
	{
		public Tags(ObservableCollection<Footwork> footwork,
			ObservableCollection<Technique> techniques,
			ObservableCollection<Target> targets,
			ObservableCollection<Guard> guards,
			ObservableCollection<GuardModifer> guardModifiers)
		{
			Footwork = footwork;
			Techniques = techniques;
			Targets = targets;
			Guards = guards;
			GuardModifiers = guardModifiers;
		}

		public ObservableCollection<Footwork> Footwork { get; }
		public ObservableCollection<Technique> Techniques { get; }
		public ObservableCollection<Target> Targets { get; }
		public ObservableCollection<Guard> Guards { get; }
		public ObservableCollection<GuardModifer> GuardModifiers { get; }
	}
}