using HemaVideoLib.Models;
using System;
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
	}
}