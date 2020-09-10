using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Domain.Entities;

namespace CourseLibrary.Common.Extensions.Mappings
{
    public static class CourseMapping
    {
        public static CourseDto ToDto(this CourseEntity course)
        {
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                AuthorId = course.AuthorId
            };
        }
    }
}