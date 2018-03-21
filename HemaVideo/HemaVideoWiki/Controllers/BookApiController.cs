using HemaVideoLib.Models;
using HemaVideoLib.Services;
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

        public BookApiController(BookService bookService)
        {
            m_BookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
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



    }
}

