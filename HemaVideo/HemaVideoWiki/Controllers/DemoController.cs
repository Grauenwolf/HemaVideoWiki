using HemaVideoLib.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("demo")]
    public class DemoController : Controller
    {
        readonly BookService m_BookService;

        public DemoController(BookService bookService)
        {
            m_BookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public async Task<IActionResult> Index()
        {
            var model = await m_BookService.GetBooksAndAuthorsAsync();
            return View(model);
        }

        [HttpGet("book/{bookKey}")]
        public async Task<IActionResult> Book([FromRoute]int bookKey)
        {
            var model = await m_BookService.GetBookDetailAsync(bookKey);
            return View(model);
        }

        [HttpGet("book/{bookKey}/section/{sectionKey}")]
        public async Task<IActionResult> Section([FromRoute]int bookKey, [FromRoute] int sectionKey)
        {
            var model = await m_BookService.GetSectionDetailAsync(sectionKey);
            return View(model);
        }
    }
}