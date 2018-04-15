using HemaVideoLib.Models;
using HemaVideoLib.Services;
using HemaVideoWiki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[Route("demo")]
	public class DemoController : SecureController
	{
		private readonly BookService m_BookService;
		private readonly PlayService m_PlayService;

		public DemoController(BookService bookService, PlayService playService, UserManager<ApplicationUser> userManager) : base(userManager)
		{
			m_BookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
			m_PlayService = playService ?? throw new ArgumentNullException(nameof(playService));
		}

		public async Task<IActionResult> Index()
		{
			var model = await m_BookService.GetBooksAndAuthorsAsync(await GetCurrentUserAsync());
			return View(model);
		}

		[HttpGet("book/{bookKey}/play/search")]
		public async Task<IActionResult> Search([FromRoute] int bookKey, [FromQuery] int? guardKey = null, [FromQuery] int? techniqueKey = null, [FromQuery] int? footworkKey = null, [FromQuery]  int? targetKey = null)
		{
			//bookKey: number, guardKey: number, techniqueKey: number, footworkKey: number, targetKey: number

			var searchCriteria = new PlaySearchCriteria()
			{
				BookKey = bookKey,
				GuardKey = guardKey,
				TechniqueKey = techniqueKey,
				FootworkKey = footworkKey,
				TargetKey = targetKey
			};

			//if (searchCriteria.IsEmpty) //do we really want to support an empty search page?
			//	return View(new List<PlaySummary>());
			//else
			//{
			var model = await m_PlayService.PlaySearchAsync(searchCriteria, await GetCurrentUserAsync());
			return View(model);
			//}
		}

		[HttpGet("book/{bookKey}")]
		public async Task<IActionResult> Book([FromRoute]int bookKey, [FromQuery] int? weaponKey = null, [FromQuery] int? secondaryWeaponKey = null)
		{
			var model = await m_BookService.GetBookDetailAsync(bookKey, weaponKey.HasValue, await GetCurrentUserAsync());

			if (weaponKey.HasValue)
			{
				model.Sections.FilterSectionsByWeapon(weaponKey.Value, secondaryWeaponKey);
			}

			model.Stats = await m_PlayService.GetStats(bookKey, await GetCurrentUserAsync());

			return View(model);
		}

		[HttpGet("book/{bookKey}/section/{sectionKey}")]
		public async Task<IActionResult> Section([FromRoute]int bookKey, [FromRoute] int sectionKey, [FromQuery] int? weaponKey = null, [FromQuery] int? secondaryWeaponKey = null)
		{
			var model = await m_BookService.GetSectionDetailAsync(sectionKey, weaponKey.HasValue, true, await GetCurrentUserAsync());

			if (weaponKey.HasValue)
			{
				model.Subsections.FilterSectionsByWeapon(weaponKey.Value, secondaryWeaponKey);
			}

			return View(model);
		}
	}
}