namespace HemaVideoTools.Services
{
	partial class Technique
	{
		public string TechniqueFullName => Formatter.MultiPart(TechniqueName, AlternateTechniqueName);
	}

	partial class Guard
	{
		public string GuardFullName => Formatter.MultiPart(GuardName, AlternateGuardName);
	}

	partial class Measure
	{
		public string MeasureFullName => Formatter.MultiPart(MeasureName, AlternateMeasureName);
	}

	partial class PlayStep
	{
		public PlayStep()
		{
			PropertyChanged += PlayStep_PropertyChanged;
		}

		private void PlayStep_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Actor))
				RaisePropertyChanged(nameof(IsAgent));
		}

		public bool IsAgent
		{
			get
			{
				return Actor != "P";
			}
			set
			{
				//Setting the Actor property raises the property changed notification for this as well.
				if (value)
					Actor = "A";
				else
					Actor = "P";
			}
		}
	}
}