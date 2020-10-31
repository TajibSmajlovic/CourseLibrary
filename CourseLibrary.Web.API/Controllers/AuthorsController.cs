using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using System.Dynamic;

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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AuthorSearchRequest request)
        {
            PagedList<AuthorDto> result = null;
            PagedList<ExpandoObject> result2 = null;

            if (!string.IsNullOrEmpty(request.Fields))
                result2 = await _authorService.GetPagedWithFieldsAsync(request);
            else
                result = await _authorService.GetPagedAsync(request);

            if (result == null && result2 == null)
                return NotFound();

            if (result != null)
                return Ok(result);

            return Ok(result2);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public async Task<ActionResult<AuthorDto>> GetAuthor(Guid id)
        {
            var author = await _authorService.GetAuthorAsync(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorDto), 200)]
        public async Task<ActionResult> AddAuthor(CreateAuthorRequest author)
        {
            if (author == null)
                return BadRequest();

            await _authorService.AddAuthorAsync(author);

            return Ok(author);
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(Guid authorId, [FromBody] AuthorUpdateRequest authorRequest)
        {
            AuthorDto author = await _authorService.UpdateAuthorAsync(authorId, authorRequest);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            await _authorService.DeleteAuthorAsync(authorId);

            return Ok();
        }
    }
}