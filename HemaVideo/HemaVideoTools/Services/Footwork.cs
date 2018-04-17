namespace HemaVideoTools.Services
{
	partial class Footwork : ITag
	{
		public string FootworkFullName => Formatter.MultiPart(FootworkName, AlternateFootworkName);

		int? ITag.Key { get => FootworkKey; set => FootworkKey = value; }
		string ITag.Name { get => FootworkName; set => FootworkName = value; }
		string ITag.AlternateName { get => AlternateFootworkName; set => AlternateFootworkName = value; }
		bool ITag.HasAlternateName => true;
	}
}