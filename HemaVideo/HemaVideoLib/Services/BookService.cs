using HemaVideoLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Anchor;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class BookService
	{
		private readonly SqlServerDataSource m_DataSource;

		public BookService(SqlServerDataSource dataSource)
		{
			m_DataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
		}

		public async Task<BookDetail> GetBookDetailAsync(int bookKey, bool includeWeaponsBySection)
		{
			var filter = new { bookKey };
			var book = await m_DataSource.From("Sources.Book", filter).ToObject<BookDetail>().ExecuteAsync();

			book.Authors.AddRange(await m_DataSource.From("Sources.BookAuthorDetail", filter).ToCollection<Author>().ExecuteAsync());

			book.Sections.AddRange(await GetSectionsAsync(bookKey, includeWeaponsBySection));

			book.Weapons.AddRange(await m_DataSource.From("Sources.BookWeaponDetail", filter).WithSorting("PrimaryWeaponName").ToCollection<WeaponVersus>().ExecuteAsync());

			return book;
		}

		public Task<List<BookAuthorDetail>> GetBooksAndAuthorsAsync()
		{
			return m_DataSource.From("Sources.BookAuthorDetail").WithSorting("BookName", "AuthorName").ToCollection<BookAuthorDetail>().ExecuteAsync();
		}

		public Task<List<BookSummary>> GetBooksAsync()
		{
			return m_DataSource.From("Sources.Book").WithSorting("BookName").ToCollection<BookSummary>().ExecuteAsync();
		}

		public Task<BookSummary> GetBookSummaryAsync(int bookKey)
		{
			var filter = new { bookKey };
			return m_DataSource.From("Sources.Book", filter).ToObject<BookSummary>().ExecuteAsync();
		}

		//public Task<SectionSummary> GetSectionSummaryAsync(int sectionKey)
		//{
		//    throw new NotImplementedException();
		//}

		public async Task<SectionDetail> GetSectionDetailAsync(int sectionKey, bool includeWeapons)
		{
			var section = await m_DataSource.From("Sources.SectionDetail", new { sectionKey }).ToObject<SectionDetail>().ExecuteAsync();
			section.Subsections.AddRange(await GetSubsectionsAsync(section.BookKey, section.SectionKey, includeWeapons));
			section.Videos.AddRange(await m_DataSource.From("Interpretations.Video", new { sectionKey }).ToCollection<Video>().ExecuteAsync());
			return section;
		}

		private async Task<List<SectionSummary>> GetSectionsAsync(int bookKey, bool includeWeapons)
		{
			var filter = new { bookKey };
			var sections = await m_DataSource.From("Sources.SectionDetail", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();

			foreach (var section in sections)
				section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));

			if (includeWeapons)
			{
				var weapons = await m_DataSource.From("Sources.SectionWeaponMapDetail", filter).ToCollection<WeaponVersusSummary>().ExecuteAsync();
				foreach (var section in sections)
					section.Weapons.AddRange(weapons.Where(x => x.SectionKey == section.SectionKey));
			}

			var result = sections.Where(x => x.ParentSectionKey == null).ToList();
			foreach (var section in result)
				section.Depth = 1;
			return result;
		}

		private async Task<List<SectionSummary>> GetSubsectionsAsync(int bookKey, int sectionKey, bool includeWeapons)
		{
			//This could be more efficient using a recursive CTE

			var filter = new { bookKey };
			var sections = await m_DataSource.From("Sources.SectionDetail", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();
			foreach (var section in sections)
				section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));

			if (includeWeapons)
			{
				var weapons = await m_DataSource.From("Sources.SectionWeaponMapDetail", filter).ToCollection<WeaponVersusSummary>().ExecuteAsync();
				foreach (var section in sections)
					section.Weapons.AddRange(weapons.Where(x => x.SectionKey == section.SectionKey));
			}

			List<SectionSummary> result = sections.Where(x => x.ParentSectionKey == sectionKey).ToList();

			foreach (var section in result)
				section.Depth = 1;

			return result;
		}
	}
}