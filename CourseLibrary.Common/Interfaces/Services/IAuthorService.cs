using System;
using System.Dynamic;
using System.Threading.Tasks;
using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;

namespace CourseLibrary.Common.Interfaces
{
    public interface IAuthorService
    {
        Task<PagedList<AuthorDto>> GetPagedAsync(AuthorSearchRequest request);

        Task<PagedList<ExpandoObject>> GetPagedWithFieldsAsync(AuthorSearchRequest request);

        Task<AuthorDto> GetAuthorAsync(Guid authorId);

        Task AddAuthorAsync(CreateAuthorRequest author);

        Task<AuthorDto> UpdateAuthorAsync(Guid authorId, AuthorUpdateRequest authorRequest);

        Task DeleteAuthorAsync(Guid authorId);
    }
}