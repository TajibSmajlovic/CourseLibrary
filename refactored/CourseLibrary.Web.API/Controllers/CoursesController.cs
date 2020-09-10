using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseLibrary.Web.API.Controllers
{
    [Route("api/authours/{authorId}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _coursesService;

        public CoursesController(ICourseService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            //if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();

            var courses = _coursesService.GetCourses(authorId);

            if (courses == null)
                return NotFound();

            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        public ActionResult<CourseDto> GetAuthorsCourse(Guid authorId, Guid id)
        {
            //if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();

            var course = _coursesService.GetCourse(authorId, id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }
    }
}