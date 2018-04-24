using System.Collections.ObjectModel;

namespace HemaVideoTools.Services
{
	partial class BookDetail
	{
		public SectionSummary FindSection(int sectionKey)
		{
			return FindSection(Sections, sectionKey);
		}

		SectionSummary FindSection(ObservableCollection<SectionSummary> sections, int sectionKey)
		{
			foreach (var section in sections)
			{
				if (section.SectionKey == sectionKey)
					return section;
				var result = FindSection(section.Subsections, sectionKey);
				if (result != null)
					return result;
			}
			return null;
		}
	}
}