using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Mappings;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorseLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ICourseLibraryContext _context;

        public AuthorService(ICourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagedList<AuthorDto>> GetPagedAsync(AuthorSearchRequest request)
        {
            var query = _context.Authors.AsNoTracking()
                                      .AsQueryable();

            query = ApplyFilter(query, request);

            List<AuthorDto> list = await query.Select(x => x.ToDto())
                                            .ToListAsync();

            var result = new PagedList<AuthorDto>(list);

            return result;
        }

        public async Task<AuthorDto> GetAuthorAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            AuthorEntity author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            return author.ToDto();
        }

        public async Task AddAuthorAsync(AuthorCreationDto author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            _context.Authors.Add(author.ToEntity());

            await _context.SaveChangesAsync();
        }

        public void UpdateAuthor(AuthorEntity author)
        {
            // no code in this implementation
        }

        public void DeleteAuthor(AuthorEntity author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            _context.Authors.Remove(author);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        #region Private methods

        private IQueryable<AuthorEntity> ApplyFilter(IQueryable<AuthorEntity> query, AuthorSearchRequest request)
        {
            if (!string.IsNullOrEmpty(request.Term))
                query = query.Where(x => x.FirstName.ToLower().Contains(request.Term.Trim().ToLower())
                || x.LastName.ToLower().Contains(request.Term.Trim().ToLower())
                || x.MainCategory.ToLower().Contains(request.Term.Trim().ToLower()));

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(x => x.FirstName.ToLower().Contains(request.Name.Trim().ToLower())
                || x.LastName.ToLower().Contains(request.Name.Trim().ToLower()));

            if (!string.IsNullOrEmpty(request.MainCategory))
                query = query.Where(x => x.MainCategory.ToLower().Contains(request.MainCategory.Trim().ToLower()));

            return query;
        }

        #endregion Private methods
    }
}