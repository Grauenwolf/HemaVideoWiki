using HemaVideoLib.Models;
using HemaVideoLib.Services;
using HemaVideoWiki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
	[Produces("application/json")]
	[Route("api/tag")]
	public class TagApiController : SecureController
	{
		private readonly TagsService m_TagsService;

		public TagApiController(TagsService tagsService, UserManager<ApplicationUser> userManager) : base(userManager)
		{
			m_TagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
		}

		[HttpGet("weapon")]
		public async Task<List<Weapon>> GetWeaponsAsync()
		{
			return await m_TagsService.GetWeaponsAsync(await GetCurrentUserAsync());
		}

		[HttpGet("technique")]
		public async Task<List<Technique>> GetTechniquesAsync()
		{
			return await m_TagsService.GetTechniquesAsync(await GetCurrentUserAsync());
		}

		[HttpGet("target")]
		public async Task<List<Target>> GetTargetsAsync()
		{
			return await m_TagsService.GetTargetsAsync(await GetCurrentUserAsync());
		}

		[HttpGet("guard")]
		public async Task<List<Guard>> GetGuardsAsync()
		{
			return await m_TagsService.GetGuardsAsync(await GetCurrentUserAsync());
		}

		[HttpGet("guardModifier")]
		public async Task<List<GuardModifer>> GetGuardModifersAsync()
		{
			return await m_TagsService.GetGuardModifersAsync(await GetCurrentUserAsync());
		}

		[HttpGet("footwork")]
		public async Task<List<Footwork>> GetFootworkAsync()
		{
			return await m_TagsService.GetFootworkAsync(await GetCurrentUserAsync());
		}
	}
}