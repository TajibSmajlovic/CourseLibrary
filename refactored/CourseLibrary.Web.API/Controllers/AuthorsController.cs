﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;

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
        public async Task<ActionResult<PagedList<AuthorDto>>> Get([FromQuery] AuthorSearchRequest request)
        {
            PagedList<AuthorDto> result = await _authorService.GetPagedAsync(request);

            if (result == null)
                return NotFound();

            return Ok(result);
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
        public async Task<ActionResult> AddAuthor(AuthorCreationDto author)
        {
            if (author == null)
                return BadRequest();

            await _authorService.AddAuthorAsync(author);

            return Ok(author);
        }
    }
}