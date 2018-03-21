//using HemaVideoLib;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace HemaVideoWiki.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/section")]
//    public class SectionApiController : Controller
//    {
//        readonly BookService m_BookService;

//        public SectionApiController(BookService BookService)
//        {
//            m_BookService = BookService ?? throw new ArgumentNullException(nameof(BookService));
//        }

//        //[HttpGet("{SectionKey}")]
//        //public Task<SectionSummary> GetSection([FromRoute] int SectionKey)
//        //{
//        //    return m_BookService.GetSectionSummaryAsync(SectionKey);
//        //}

//        [HttpGet("{SectionKey}/detail")]
//        public Task<SectionDetail> GetSectionDetail([FromRoute] int SectionKey)
//        {
//            return m_BookService.GetSectionDetailAsync(SectionKey);
//        }


//    }
//}

