namespace HemaVideoLib.Models
{
	public class Translation
	{
		public int TranslationKey { get; set; }
		public int BookKey { get; set; }
		public int TranslatorKey { get; set; }
		public string TranslationTitle { get; set; }
		public string TranslationUrl { get; set; }
		public string Copyright { get; set; }
		public string TranslatorName { get; set; }
	}
}