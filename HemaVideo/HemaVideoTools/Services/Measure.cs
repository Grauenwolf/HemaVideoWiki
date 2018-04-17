namespace HemaVideoTools.Services
{
	partial class Measure : ITag
	{
		public string MeasureFullName => Formatter.MultiPart(MeasureName, AlternateMeasureName);

		int? ITag.Key { get => MeasureKey; set => MeasureKey = value; }
		string ITag.Name { get => MeasureName; set => MeasureName = value; }
		string ITag.AlternateName { get => AlternateMeasureName; set => AlternateMeasureName = value; }
		bool ITag.HasAlternateName => true;
	}
}