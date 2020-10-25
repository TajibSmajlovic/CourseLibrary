using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Extensions;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Domain.Entities;

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

        public static AuthorEntity ToEntity(this AuthorCreationDto author)
        {
            if (author == null)
                return null;

            return new AuthorEntity
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                MainCategory = author.MainCategory
            };
        }
    }
}