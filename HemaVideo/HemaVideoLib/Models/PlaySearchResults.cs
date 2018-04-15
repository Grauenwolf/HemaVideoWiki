using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class PlaySearchResults
	{
		public PlaySearchCriteria SearchCriteria { get; set; }

		public BookSummary Book { get; set; }
		public List<string> SearchTerms { get; } = new List<string>();

		public List<PlaySummary> Plays { get; } = new List<PlaySummary>();
	}
}