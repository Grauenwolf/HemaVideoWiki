namespace HemaVideoTools.Services
{
	partial class GuardModifier : ITag
	{
		int? ITag.Key { get => GuardModifierKey; set => GuardModifierKey = value; }
		string ITag.Name { get => GuardModifierName; set => GuardModifierName = value; }
		string ITag.AlternateName { get; set; } //ignored
		bool ITag.HasAlternateName => false;
	}
}