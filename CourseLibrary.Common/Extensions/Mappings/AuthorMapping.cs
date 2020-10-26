using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Extensions;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;
using System.Linq;

namespace CourseLibrary.Common.Mappings
{
    public static class AuthorMapping
    {
        public static AuthorDto ToDto(this AuthorEntity author)
        {
            if (author == null)
                return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                Age = author.DateOfBirth.GetCurrentAge(),
                MainCategory = author.MainCategory,
            };
        }

        public static AuthorEntity ToEntity(this CreateAuthorRequest author)
        {
            if (author == null)
                return null;

            return new AuthorEntity
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                MainCategory = author.MainCategory,
                Courses = author.Courses.Select(x => new CourseEntity
                {
                    Title = x.Title,
                    Description = x.Description,
                    AuthorId = author.Id
                }).ToList()
            };
        }

        public static void ToUpdateEntity(this AuthorUpdateRequest request, ref AuthorEntity author)
        {
            if (request == null || author == null)
                return;

            if (!string.IsNullOrEmpty(request.FirstName))
                author.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                author.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.LastName))
                author.MainCategory = request.MainCategory;

            if (!string.IsNullOrEmpty(request.LastName))
                author.DateOfBirth = request.DateOfBirth;
        }
    }
}