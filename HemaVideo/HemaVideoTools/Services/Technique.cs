namespace HemaVideoTools.Services
{
	partial class Technique : ITag
	{
		public string TechniqueFullName => Formatter.MultiPart(TechniqueName, AlternateTechniqueName);

		int? ITag.Key { get => TechniqueKey; set => TechniqueKey = value; }
		string ITag.Name { get => TechniqueName; set => TechniqueName = value; }
		string ITag.AlternateName { get => AlternateTechniqueName; set => AlternateTechniqueName = value; }
		bool ITag.HasAlternateName => true;
	}
}