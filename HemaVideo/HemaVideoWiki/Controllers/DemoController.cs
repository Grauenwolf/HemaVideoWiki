using HemaVideoLib.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HemaVideoWiki.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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
    }
}