using System;
using System.Threading.Tasks;
using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;

namespace CourseLibrary.Common.Interfaces
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>> GetPagedAsync(AuthorSearchRequest request);

        Task<AuthorDto> GetAuthorAsync(Guid authorId);

        Task AddAuthorAsync(AuthorCreationDto author);

        void UpdateAuthor(AuthorEntity author);

        void DeleteAuthor(AuthorEntity author);
    }
}