using System.Collections.Generic;

namespace HemaVideoLib.Models
{
	public class BookStats
	{
		public List<Tag> Techniques { get; set; }
		public List<Tag> Guards { get; set; }
		public List<Tag> Footwork { get; set; }
		public List<Tag> Targets { get; set; }
	}
}