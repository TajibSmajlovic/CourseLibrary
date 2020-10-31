using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CourseLibrary.Common.Constants;
using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Mappings;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;
using CourseLibrary.Common.Enums;
using System.Dynamic;
using CourseLibrary.Common.Extensions;

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
            var query = _context.Authors.AsNoTracking();

            ApplyFiltering(ref query, request);
            ApplyOrdering(ref query, request);
            ApplyPaging(ref query, request);

            List<AuthorDto> list = await query.Select(x => x.ToDto())
                                            .ToListAsync();

            return new PagedList<AuthorDto>(list);
        }

        public async Task<PagedList<ExpandoObject>> GetPagedWithFieldsAsync(AuthorSearchRequest request)
        {
            var query = _context.Authors.AsNoTracking();

            ApplyFiltering(ref query, request);
            ApplyOrdering(ref query, request);
            ApplyPaging(ref query, request);

            List<AuthorDto> list = await query.Select(x => x.ToDto())
                                            .ToListAsync();

            return new PagedList<ExpandoObject>(list.ShapeData(request.Fields).ToList());
        }

        public async Task<AuthorDto> GetAuthorAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            AuthorEntity author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            return author.ToDto();
        }

        public async Task AddAuthorAsync(CreateAuthorRequest author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            author.Id = Guid.NewGuid();

            _context.Authors.Add(author.ToEntity());

            await _context.SaveChangesAsync();
        }

        public async Task<AuthorDto> UpdateAuthorAsync(Guid authorId, AuthorUpdateRequest authorRequest)

        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            AuthorEntity author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            if (author == null)
                return null;

            authorRequest.ToUpdateEntity(ref author);

            await _context.SaveChangesAsync();

            return author.ToDto();
        }

        public async Task DeleteAuthorAsync(Guid authorId)
        {
            AuthorEntity author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            if (author == null)
                throw new ArgumentNullException(nameof(author));

            foreach (var course in author.Courses)
            {
                course.IsDeleted = true;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
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

        private void ApplyFiltering(ref IQueryable<AuthorEntity> query, AuthorSearchRequest request)
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
        }

        private void ApplyOrdering(ref IQueryable<AuthorEntity> query, AuthorSearchRequest request)
        {
            switch (request.OrderBy)
            {
                case AuthorOrderBy.AGE:
                    if (request.SortOrder == SortOrder.Descending)
                        query = query.OrderByDescending(a => a.DateOfBirth);
                    else
                        query = query.OrderBy(a => a.DateOfBirth);

                    break;

                case AuthorOrderBy.MAIN_CATEGORY:
                    if (request.SortOrder == SortOrder.Descending)
                        query = query.OrderByDescending(a => a.MainCategory);
                    else
                        query = query.OrderBy(a => a.MainCategory);

                    break;

                default:
                    if (request.SortOrder == SortOrder.Descending)
                        query = query.OrderByDescending(a => a.FirstName).ThenBy(a => a.LastName);
                    else
                        query = query.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);

                    break;
            }
        }

        private void ApplyPaging(ref IQueryable<AuthorEntity> query, AuthorSearchRequest request)
        {
            query = query.Skip(request.PageSize * (request.PageNumber - 1))
                 .Take(request.PageSize);
        }

        #endregion Private methods
    }
}