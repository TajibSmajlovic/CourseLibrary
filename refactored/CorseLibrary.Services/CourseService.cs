using CourseLibrary.Common.Extensions.Mappings;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Database;
using CourseLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorseLibrary.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseLibraryContext _context;
        //private readonly IPropertyMappingService _propertyMappingService;

        public CourseService(ICourseLibraryContext context
            // IPropertyMappingService propertyMappingService
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            // _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void AddCourse(Guid authorId, CourseEntity course)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (course == null)
                throw new ArgumentNullException(nameof(course));

            // always set the AuthorId to the passed-in authorId
            course.AuthorId = authorId;
            _context.Courses.Add(course);
        }

        public void DeleteCourse(CourseEntity course)
        {
            _context.Courses.Remove(course);
        }

        public CourseDto GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (courseId == Guid.Empty)
                throw new ArgumentNullException(nameof(courseId));

            if (!AuthorExists(authorId))
                return null;

            return _context.Courses
              .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault().ToDto();
        }

        public IEnumerable<CourseDto> GetCourses(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (!AuthorExists(authorId))
                return null;

            return _context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToList().Select(x => x.ToDto());
        }

        public void UpdateCourse(CourseEntity course)
        {
            // no code in this implementation
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            return _context.Authors.Any(a => a.Id == authorId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}