using HemaVideoLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class PlayService : ServiceBase
	{
		public PlayService(SqlServerDataSource dataSource) : base(dataSource)
		{
		}

		public async Task<List<PlayDetail>> GetPlayDetailsForSectionAsync(int sectionKey, IUser currentUser)
		{
			var filter = new { sectionKey };
			var dataSource = DataSource(currentUser);
			var plays = await dataSource.From("Interpretations.PlayDetail", filter).WithSorting("VariantName").ToCollection<PlayDetail>().ExecuteAsync();

			var steps = await dataSource.From("Interpretations.PlayStepDetail", filter).WithSorting("PlayKey", "TempoNumber", "Actor").ToCollection<PlayStepDetail>().ExecuteAsync();

			foreach (var play in plays)
				play.Steps.AddRange(steps.Where(x => x.PlayKey == play.PlayKey));

			return plays;
		}

		public async Task<PlayDetail> GetPlayDetailAsync(int playKey, IUser currentUser)
		{
			var filter = new { playKey };
			var dataSource = DataSource(currentUser);
			var play = await dataSource.From("Interpretations.PlayDetail", filter).ToObject<PlayDetail>().ExecuteAsync();

			play.Steps.AddRange(await dataSource.From("Interpretations.PlayStepDetail", filter).ToCollection<PlayStepDetail>().ExecuteAsync());

			return play;
		}

		public async Task<int> UpdatePlayAsync(Play play, IUser currentUser)
		{
			if (string.IsNullOrEmpty(play.VariantName))
				play.VariantName = null;

			if (play.PlayKey.HasValue)
				await CheckPermissionPlayAsync(play.PlayKey.Value, currentUser);
			else
				await CheckPermissionSectionAsyc(play.SectionKey, currentUser);

			using (var dataSource = await DataSource(currentUser).BeginTransactionAsync())
			{
				if (play.PlayKey.HasValue) //Update
				{
					await dataSource.Update("Interpretations.Play", play).ExecuteAsync();
					//Replace all steps
					await dataSource.DeleteWithFilter("Interpretations.PlayStep", "PlayKey = @PlayKey", new { PlayKey = play.PlayKey.Value }).ExecuteAsync();
				}
				else
				{
					play.PlayKey = await dataSource.Insert("Interpretations.Play", play).ToInt32().ExecuteAsync();
				}

				foreach (var step in play.Steps)
				{
					step.PlayKey = play.PlayKey.Value;
					await dataSource.Insert("Interpretations.PlayStep", step).ExecuteAsync();
				}

				dataSource.Commit();
			}
			return play.PlayKey.Value;
		}

		public async Task<BookStats> GetStats(int bookKey, IUser currentUser)
		{
			return new BookStats()
			{
				Footwork = await FootworkByBook(bookKey, currentUser),
				Guards = await GuardsByBook(bookKey, currentUser),
				Techniques = await TechniquesByBook(bookKey, currentUser),
				Targets = await TargetsByBook(bookKey, currentUser)
			};
		}

		public async Task<List<Tag>> TechniquesByBook(int bookKey, IUser currentUser)
		{
			return await DataSource(currentUser).From("Interpretations.TechniquesByBook", new { bookKey }).WithSorting("Name", "AlternateName").ToCollection<Tag>().ExecuteAsync();
		}

		public async Task<List<Tag>> FootworkByBook(int bookKey, IUser currentUser)
		{
			return await DataSource(currentUser).From("Interpretations.FootworkByBook", new { bookKey }).WithSorting("Name", "AlternateName").ToCollection<Tag>().ExecuteAsync();
		}

		public async Task<List<Tag>> TargetsByBook(int bookKey, IUser currentUser)
		{
			return await DataSource(currentUser).From("Interpretations.TargetsByBook", new { bookKey }).WithSorting("Name", "AlternateName").ToCollection<Tag>().ExecuteAsync();
		}

		public async Task<List<Tag>> GuardsByBook(int bookKey, IUser currentUser)
		{
			return await DataSource(currentUser).From("Interpretations.GuardsByBook", new { bookKey }).WithSorting("Name", "AlternateName").ToCollection<Tag>().ExecuteAsync();
		}

		public async Task<PlaySearchResults> PlaySearchAsync(PlaySearchCriteria searchCriteria, IUser currentUser)
		{
			var ds = DataSource(currentUser);
			var result = new PlaySearchResults() { SearchCriteria = searchCriteria };

			var filterParts = new List<string>();
			if (searchCriteria.BookKey.HasValue)
				filterParts.Add("BookKey = @BookKey");
			if (searchCriteria.FootworkKey.HasValue)
			{
				filterParts.Add("EXISTS ( SELECT * FROM Interpretations.PlayStep ps WHERE PlayDetail.PlayKey = ps.PlayKey AND ps.FootworkKey = @FootworkKey)");
				result.SearchTerms.Add(await ds.GetByKey("Tags.Footwork", searchCriteria.FootworkKey.Value).ToString("FootworkName").ExecuteAsync());
			}
			if (searchCriteria.TechniqueKey.HasValue)
			{
				filterParts.Add("EXISTS ( SELECT * FROM Interpretations.PlayStep ps WHERE PlayDetail.PlayKey = ps.PlayKey AND  @TechniqueKey IN (ps.TechniqueKey1, ps.TechniqueKey2, ps.TechniqueKey3))");
				result.SearchTerms.Add(await ds.GetByKey("Tags.Technique", searchCriteria.TechniqueKey.Value).ToString("TechniqueName").ExecuteAsync());
			}
			if (searchCriteria.GuardKey.HasValue)
			{
				filterParts.Add("(@GuardKey IN (AGuardKey, PGuardKey) OR EXISTS ( SELECT * FROM Interpretations.PlayStep ps WHERE PlayDetail.PlayKey = ps.PlayKey AND @GuardKey in (ps.GuardKey, ps.IntermediateGuardKey)))");
				result.SearchTerms.Add(await ds.GetByKey("Tags.Guard", searchCriteria.GuardKey.Value).ToString("GuardName").ExecuteAsync());
			}
			if (searchCriteria.TargetKey.HasValue)
			{
				filterParts.Add("EXISTS ( SELECT * FROM Interpretations.PlayStep ps WHERE PlayDetail.PlayKey = ps.PlayKey AND @TargetKey IN (ps.TargetKey1, ps.TargetKey2, ps.TargetKey3))");
				result.SearchTerms.Add(await ds.GetByKey("Tags.Target", searchCriteria.TargetKey.Value).ToString("TargetName").ExecuteAsync());
			}

			result.Plays.AddRange(await ds.From("Interpretations.PlayDetail", string.Join(" AND ", filterParts), searchCriteria).ToCollection<PlaySummary>().ExecuteAsync());

			if (searchCriteria.BookKey.HasValue)
				result.Book = await ds.From("Sources.Book", new { searchCriteria.BookKey }).ToObject<BookSummary>().ExecuteAsync();

			return result;
		}
	}
}