using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Extensions;
using CourseLibrary.Domain.Entities;

namespace CourseLibrary.Common.Mappings
{
    public static class AuthorMapping
    {
        public static AuthorDto ToDto(this AuthorEntity author)
        {
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                Age = author.DateOfBirth.GetCurrentAge(),
                MainCategory = author.MainCategory,
            };
        }
    }
}