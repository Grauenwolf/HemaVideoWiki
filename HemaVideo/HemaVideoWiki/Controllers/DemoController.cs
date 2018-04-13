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

		public DemoController(BookService bookService, UserManager<ApplicationUser> userManager) : base(userManager)
		{
			m_BookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
		}

		public async Task<IActionResult> Index()
		{
			var model = await m_BookService.GetBooksAndAuthorsAsync(await GetCurrentUserAsync());
			return View(model);
		}

		[HttpGet("book/{bookKey}")]
		public async Task<IActionResult> Book([FromRoute]int bookKey, [FromQuery] int? weaponKey = null, [FromQuery] int? secondaryWeaponKey = null)
		{
			var model = await m_BookService.GetBookDetailAsync(bookKey, weaponKey.HasValue, await GetCurrentUserAsync());

			if (weaponKey.HasValue)
			{
				model.Sections.FilterSectionsByWeapon(weaponKey.Value, secondaryWeaponKey);
			}

			return View(model);
		}

		[HttpGet("book/{bookKey}/section/{sectionKey}")]
		public async Task<IActionResult> Section([FromRoute]int bookKey, [FromRoute] int sectionKey, [FromQuery] int? weaponKey = null, [FromQuery] int? secondaryWeaponKey = null)
		{
			var model = await m_BookService.GetSectionDetailAsync(sectionKey, weaponKey.HasValue, await GetCurrentUserAsync());

			if (weaponKey.HasValue)
			{
				model.Subsections.FilterSectionsByWeapon(weaponKey.Value, secondaryWeaponKey);
			}

			return View(model);
		}
	}
}