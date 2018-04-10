using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class SectionDetail : SectionSummary
	{
		public string BookName { get; set; }
		public List<Video> Videos { get; } = new List<Video>();
	}
}