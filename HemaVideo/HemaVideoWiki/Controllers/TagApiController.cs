using HemaVideoLib.Models;
using HemaVideoLib.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
	[Produces("application/json")]
	[Route("api/tag")]
	public class TagApiController : Controller
	{
		private readonly TagsService m_TagsService;

		public TagApiController(TagsService tagsService)
		{
			m_TagsService = tagsService ?? throw new ArgumentNullException(nameof(tagsService));
		}

		[HttpGet("weapon")]
		public Task<List<Weapon>> GetWeaponsAsync()
		{
			return m_TagsService.GetWeaponsAsync();
		}

		[HttpGet("technique")]
		public Task<List<Technique>> GetTechniquesAsync()
		{
			return m_TagsService.GetTechniquesAsync();
		}

		[HttpGet("target")]
		public Task<List<Target>> GetTargetsAsync()
		{
			return m_TagsService.GetTargetsAsync();
		}

		[HttpGet("guard")]
		public Task<List<Guard>> GetGuardsAsync()
		{
			return m_TagsService.GetGuardsAsync();
		}

		[HttpGet("guardModifier")]
		public Task<List<GuardModifer>> GetGuardModifersAsync()
		{
			return m_TagsService.GetGuardModifersAsync();
		}

		[HttpGet("footwork")]
		public Task<List<Footwork>> GetFootworkAsync()
		{
			return m_TagsService.GetFootworkAsync();
		}
	}
}