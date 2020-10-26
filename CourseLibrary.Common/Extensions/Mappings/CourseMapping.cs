using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;

namespace CourseLibrary.Common.Extensions.Mappings
{
    public static class CourseMapping
    {
        public static CourseDto ToDto(this CourseEntity course)
        {
            if (course == null)
                return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                AuthorId = course.AuthorId
            };
        }

        public static CourseEntity ToEntity(this CreateCourseRequest course)
        {
            if (course == null)
                return null;

            return new CourseEntity
            {
                AuthorId = course.AuthorId,
                Title = course.Title,
                Description = course.Description
            };
        }

        public static void ToUpdateEntity(this CourseUpdateRequest request, ref CourseEntity course)
        {
            if (request == null || course == null)
                return;

            if (!string.IsNullOrEmpty(request.Title))
                course.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description))
                course.Description = request.Description;
        }
    }
}