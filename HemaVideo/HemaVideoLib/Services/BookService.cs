using HemaVideoLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
    public class BookService
    {
        readonly SqlServerDataSource m_DataSource;

        public BookService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<BookDetail> GetBookDetailAsync(int bookKey)
        {
            var filter = new { bookKey };
            var book = await m_DataSource.From("Sources.Book", filter).ToObject<BookDetail>().ExecuteAsync();

            book.Authors.AddRange(await m_DataSource.From("Sources.BookAuthorDetail", filter).ToCollection<Author>().ExecuteAsync());

            book.Sections.AddRange(await GetSectionsAsync(bookKey));

            return book;
        }

        public Task<BookSummary> GetBookSummaryAsync(int bookKey)
        {
            var filter = new { bookKey };
            return m_DataSource.From("Sources.Book", filter).ToObject<BookSummary>().ExecuteAsync();
        }

        public Task<List<BookSummary>> GetBooksAsync()
        {
            return m_DataSource.From("Sources.Book").ToCollection<BookSummary>().ExecuteAsync();
        }

        public Task<List<BookAuthorDetail>> GetBooksAndAuthorsAsync()
        {
            return m_DataSource.From("Sources.BookAuthorDetail").ToCollection<BookAuthorDetail>().ExecuteAsync();
        }

        public Task<SectionSummary> GetSectionSummaryAsync(int sectionKey)
        {
            throw new NotImplementedException();
        }

        public async Task<SectionDetail> GetSectionDetailAsync(int sectionKey)
        {
            var section = await m_DataSource.GetByKey("Sources.Section", sectionKey).ToObject<SectionDetail>().ExecuteAsync();
            section.Subsections.AddRange(await GetSubsectionsAsync(section.BookKey, section.SectionKey));
            section.Videos.AddRange(await m_DataSource.From("Interpretations.Video", new { sectionKey }).ToCollection<Video>().ExecuteAsync());
            return section;
        }

        public async Task<List<SectionSummary>> GetSectionsAsync(int bookKey)
        {
            var filter = new { bookKey };
            var sections = await m_DataSource.From("Sources.Section", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();
            foreach (var section in sections)
                section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));


            var result = sections.Where(x => x.ParentSectionKey == null).ToList();
            foreach (var section in result)
                section.Depth = 1;
            return result;
        }

        public async Task<List<SectionSummary>> GetSubsectionsAsync(int bookKey, int sectionKey)
        {
            //This could be more efficent using a recursive CTE

            var filter = new { bookKey };
            var sections = await m_DataSource.From("Sources.Section", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();
            foreach (var section in sections)
                section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));

            List<SectionSummary> result = sections.Where(x => x.ParentSectionKey == sectionKey).ToList();

            foreach (var section in result)
                section.Depth = 1;

            return result;
        }

    }
}
