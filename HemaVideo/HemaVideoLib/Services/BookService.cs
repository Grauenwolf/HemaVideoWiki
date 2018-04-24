﻿using HemaVideoLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Anchor;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class BookService : ServiceBase
	{
		private readonly PlayService m_PlayService;

		public BookService(SqlServerDataSource dataSource, PlayService playService) : base(dataSource)
		{
			m_PlayService = playService ?? throw new System.ArgumentNullException(nameof(playService));
		}

		public async Task<BookDetail> GetBookDetailAsync(int bookKey, bool includeWeaponsBySection, IUser currentUser)
		{
			var filter = new { bookKey };
			var book = await DataSource(currentUser).From("Sources.Book", filter).ToObject<BookDetail>().ExecuteAsync();

			book.Authors.AddRange(await DataSource(currentUser).From("Sources.BookAuthorDetail", filter).ToCollection<Author>().ExecuteAsync());

			book.Sections.AddRange(await GetSectionsAsync(bookKey, includeWeaponsBySection, currentUser));

			book.Weapons.AddRange(await DataSource(currentUser).From("Sources.BookWeaponDetail", filter).WithSorting("PrimaryWeaponName").ToCollection<WeaponVersus>().ExecuteAsync());

			book.CanEdit = await CanEditBookAsync(bookKey, currentUser);

			return book;
		}

		public Task<List<BookAuthorDetail>> GetBooksAndAuthorsAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Sources.BookAuthorDetail").WithSorting("BookName", "AuthorName").ToCollection<BookAuthorDetail>().ExecuteAsync();
		}

		public Task<List<BookSummary>> GetBooksAsync(IUser currentUser)
		{
			return DataSource(currentUser).From("Sources.Book").WithSorting("BookName").ToCollection<BookSummary>().ExecuteAsync();
		}

		public Task<BookSummary> GetBookSummaryAsync(int bookKey, IUser currentUser)
		{
			var filter = new { bookKey };
			return DataSource(currentUser).From("Sources.Book", filter).ToObject<BookSummary>().ExecuteAsync();
		}

		public async Task<SectionDetail> GetSectionDetailAsync(int sectionKey, bool includeSubsectionWeapons, bool includeSubsectionPlays, IUser currentUser)
		{
			var filter = new { sectionKey };
			var section = await DataSource(currentUser).From("Sources.SectionDetail", filter).ToObject<SectionDetail>().ExecuteAsync();
			section.Subsections.AddRange(await GetSubsectionsAsync(section.BookKey, section.SectionKey, includeSubsectionWeapons, includeSubsectionPlays, currentUser));

			section.Videos.AddRange(await DataSource(currentUser).From("Interpretations.Video", filter).ToCollection<Video>().ExecuteAsync());
			section.Weapons.AddRange(await DataSource(currentUser).From("Sources.SectionWeaponMapDetail", filter).ToCollection<WeaponVersus>().ExecuteAsync());

			section.Plays.AddRange(await m_PlayService.GetPlayDetailsForSectionAsync(sectionKey, currentUser));

			section.CanEdit = await CanEditBookAsync(section.BookKey, currentUser);

			return section;
		}

		private async Task<List<SectionSummary>> GetSectionsAsync(int bookKey, bool includeWeapons, IUser currentUser)
		{
			var filter = new { bookKey };
			var sections = await DataSource(currentUser).From("Sources.SectionDetail", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();

			foreach (var section in sections)
				section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));

			if (includeWeapons)
			{
				var weapons = await DataSource(currentUser).From("Sources.SectionWeaponMapDetail", filter).ToCollection<WeaponVersusSummary>().ExecuteAsync();
				foreach (var section in sections)
					section.Weapons.AddRange(weapons.Where(x => x.SectionKey == section.SectionKey));
			}

			var result = sections.Where(x => x.ParentSectionKey == null).ToList();
			foreach (var section in result)
				section.Depth = 1;
			return result;
		}

		async Task<List<SectionSummary>> GetSubsectionsAsync(int bookKey, int sectionKey, bool includeWeapons, bool includePlays, IUser currentUser)
		{
			//This could be more efficient using a recursive CTE

			var filter = new { bookKey };
			var sections = await DataSource(currentUser).From("Sources.SectionDetail", filter).WithSorting("DisplayOrder").ToCollection<SectionSummary>().ExecuteAsync();
			foreach (var section in sections)
				section.Subsections.AddRange(sections.Where(x => x.ParentSectionKey == section.SectionKey));

			if (includeWeapons)
			{
				var weapons = await DataSource(currentUser).From("Sources.SectionWeaponMapDetail", filter).ToCollection<WeaponVersusSummary>().ExecuteAsync();
				foreach (var section in sections)
					section.Weapons.AddRange(weapons.Where(x => x.SectionKey == section.SectionKey));
			}

			if (includePlays)
			{
				var plays = await DataSource(currentUser).From("Interpretations.PlayDetail", filter).ToCollection<PlaySummary>().ExecuteAsync();
				foreach (var section in sections)
					section.Plays.AddRange(plays.Where(x => x.SectionKey == section.SectionKey));
			}

			List<SectionSummary> result = sections.Where(x => x.ParentSectionKey == sectionKey).ToList();

			foreach (var section in result)
				section.Depth = 1;

			return result;
		}
	}
}