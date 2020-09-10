using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseLibrary.Common.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAuthors();

        //PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);

        Task<PagedList<AuthorDto>> GetPagedAsync(AuthorSearchRequest request);

        AuthorDto GetAuthor(Guid authorId);

        IEnumerable<AuthorEntity> GetAuthors(IEnumerable<Guid> authorIds);

        void AddAuthor(AuthorEntity author);

        void DeleteAuthor(AuthorEntity author);

        void UpdateAuthor(AuthorEntity author);

        bool Save();
    }
}