using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseLibrary.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<AuthorDto>), 200)]
        //public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        //{
        //    var authors = _authorService.GetAuthors();

        //    if (authors == null)
        //        return NotFound();

        //    return Ok(authors);
        //}

        [HttpGet]
        public async Task<ActionResult<PagedList<AuthorDto>>> Get([FromQuery] AuthorSearchRequest request)
        {
            PagedList<AuthorDto> result = await _authorService.GetPagedAsync(request);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public ActionResult<AuthorDto> getAuthor(Guid id)
        {
            var author = _authorService.GetAuthor(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }
    }
}