namespace HemaVideoLib.Models
{
	public class SectionTranslationDetail : SectionTranslation
	{
		public int BookKey { get; set; }
		public string Copyright { get; set; }
		public string TranslatorName { get; set; }
		public string TranslationTitle { get; set; }
		public int TranslatorKey { get; set; }
	}
}