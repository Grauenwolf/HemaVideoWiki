using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HemaVideoLib
{
    public class BookDetail : BookSummary
    {

        public List<Author> Authors { get; } = new List<Author>();
        public List<SectionSummary> Sections { get; } = new List<SectionSummary>();

    }
}


