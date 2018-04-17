namespace HemaVideoTools.Services
{
	partial class Target : ITag
	{
		//public string TargetFullName => Formatter.MultiPart(TargetName, AlternateTargetName);

		int? ITag.Key { get => TargetKey; set => TargetKey = value; }
		string ITag.Name { get => TargetName; set => TargetName = value; }

		//string ITag.AlternateName { get => AlternateTargetName; set => AlternateTargetName = value; }
		string ITag.AlternateName { get; set; }

		bool ITag.HasAlternateName => false;
	}
}