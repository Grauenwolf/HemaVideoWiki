using HemaVideoLib.Models;
using HemaVideoLib.Services;
using HemaVideoWiki.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
    [Produces("application/json")]
    [Route("api/book")]
    public class BookApiController : Controller
    {
        readonly BookService m_BookService;
        readonly VideoService m_VideoService;
        readonly IHttpContextAccessor m_ContextAccessor;
        readonly UserManager<ApplicationUser> m_UserManager;

        public BookApiController(BookService bookService, VideoService videoService, UserManager<ApplicationUser> userManager)
        {
            m_UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            m_VideoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
            m_BookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await m_UserManager.GetUserAsync(HttpContext.User);
        }


        [HttpGet("{bookKey}")]
        public Task<BookSummary> GetBook([FromRoute] int bookKey)
        {
            return m_BookService.GetBookSummaryAsync(bookKey);
        }

        [HttpGet("{bookKey}/detail")]
        public Task<BookDetail> GetBookDetail([FromRoute] int bookKey)
        {
            return m_BookService.GetBookDetailAsync(bookKey);
        }

        [HttpGet("")]
        public Task<List<BookSummary>> GetBooks()
        {
            return m_BookService.GetBooksAsync();
        }

        [HttpGet("{BookKey}/{SectionKey}")]
        public Task<SectionDetail> GetSectionDetail([FromRoute] int SectionKey)
        {
            return m_BookService.GetSectionDetailAsync(SectionKey);
        }


        [HttpPost("addVideo")]
        public async Task<int> AddVideo([FromBody] NewVideo video)
        {
            return await m_VideoService.AddVideo(await GetCurrentUserAsync(), video);
        }


        [HttpGet("whoAmI")]
        public async Task<ApplicationUser> WhoAmI()
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            return applicationUser;
        }

    }
}

