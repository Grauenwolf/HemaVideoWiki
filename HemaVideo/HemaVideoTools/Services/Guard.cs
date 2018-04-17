namespace HemaVideoTools.Services
{
	partial class Guard : ITag
	{
		public string GuardFullName => Formatter.MultiPart(GuardName, AlternateGuardName);

		int? ITag.Key { get => GuardKey; set => GuardKey = value; }
		string ITag.Name { get => GuardName; set => GuardName = value; }
		string ITag.AlternateName { get => AlternateGuardName; set => AlternateGuardName = value; }

		bool ITag.HasAlternateName => true;
	}
}