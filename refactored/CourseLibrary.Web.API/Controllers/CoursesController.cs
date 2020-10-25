﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;

namespace CourseLibrary.Web.API.Controllers
{
    [Route("api/authors/{authorId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _coursesService;

        public CoursesController(ICourseService coursesService)
        {
            _coursesService = coursesService;
        }

        //[HttpHead]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        public async Task<ActionResult<PagedList<CourseDto>>> GetCoursesForAuthor(Guid authorId, [FromQuery] CourseSearchRequest request)
        {
            var courses = await _coursesService.GetPagedAsync(authorId, request);

            if (courses == null)
                return NotFound();

            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        public async Task<ActionResult<CourseDto>> GetAuthorsCourse(Guid authorId, Guid id)
        {
            var course = await _coursesService.GetAuthorsCourseAsync(authorId, id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CourseCreationDto), 200)]
        public async Task<ActionResult> AddCourse(Guid authorId, CourseCreationDto course)
        {
            if (course == null || !_coursesService.AuthorExists(authorId))
                return NotFound();

            await _coursesService.AddCourseAsync(authorId, course);

            return Ok(course);
        }
    }
}