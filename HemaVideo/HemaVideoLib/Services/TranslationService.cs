using HemaVideoLib.Models;
using System.Threading.Tasks;
using Tortuga.Chain;

namespace HemaVideoLib.Services
{
	public class TranslationService : ServiceBase
	{
		public TranslationService(SqlServerDataSource dataSource) : base(dataSource)
		{
		}

		public async Task UpdateTranslationAsync(SectionTranslation translation, IUser currentUser)
		{
			var ds = DataSource(currentUser);

			await CheckPermissionSectionAsyc(translation.SectionKey, currentUser);

			var bookKey = await ds.From("Sources.Section", new { translation.SectionKey }).ToInt32("BookKey").ExecuteAsync();

			var cw = ds.From("Translations.Translation", new { bookKey, translation.TranslationKey }).ToString("Copyright").ExecuteAsync();

			if (cw == null)
				throw new MissingDataException($"Copyright information is missing for book {bookKey}, translation {translation.TranslationKey}.");

			await ds.Upsert("Translations.SectionTranslation", translation).ExecuteAsync();
		}
	}
}