using CourseLibrary.Common.Dtos;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Mappings;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Database;
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

        //private readonly IPropertyMappingService _propertyMappingService;

        public AuthorService(ICourseLibraryContext context
            // IPropertyMappingService propertyMappingService
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            // _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void AddAuthor(AuthorEntity author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            // the repository fills the id (instead of using identity columns)
            author.Id = Guid.NewGuid();

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            _context.Authors.Add(author);
        }

        public void DeleteAuthor(AuthorEntity author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            _context.Authors.Remove(author);
        }

        public AuthorDto GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            return _context.Authors.FirstOrDefault(a => a.Id == authorId).ToDto();
        }

        public IEnumerable<AuthorDto> GetAuthors()
        {
            return _context.Authors.ToList().Select(x => x.ToDto());
        }

        public async Task<PagedList<AuthorDto>> GetPagedAsync(AuthorSearchRequest request)
        {
            var query = _context.Authors.AsNoTracking()
                                      .OrderByDescending(x => x.CreatedAt)
                                      .AsQueryable();

            query = ApplyFilter(query, request);

            List<AuthorDto> list = await query.Select(x => x.ToDto())
                                            .ToListAsync();

            var result = new PagedList<AuthorDto>(list);

            return result;
        }

        //public PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        //{
        //    if (authorsResourceParameters == null) throw new ArgumentNullException(nameof(authorsResourceParameters));

        //    var collection = _context.Authors as IQueryable<Author>;

        //    if (!string.IsNullOrWhiteSpace(authorsResourceParameters.MainCategory))
        //    {
        //        var mainCategory = authorsResourceParameters.MainCategory;

        //        mainCategory = mainCategory.Trim();
        //        collection = collection.Where(a => a.MainCategory == mainCategory);
        //    }
        //    if (!string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
        //    {
        //        var searchQuery = authorsResourceParameters.SearchQuery;

        //        searchQuery = searchQuery.Trim();
        //        collection = collection.Where(a => a.MainCategory.Contains(searchQuery)
        //        || a.FirstName.Contains(searchQuery)
        //        || a.LastName.Contains(searchQuery)
        //        );
        //    }

        //    if (!string.IsNullOrWhiteSpace(authorsResourceParameters.OrderBy))
        //    {
        //        if (authorsResourceParameters.OrderBy.ToLowerInvariant() == "name") collection = collection.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);

        //        // get property mapping dictionary
        //        var authorPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<AuthorDto, Author>();

        //        collection.ApplySort(authorsResourceParameters.OrderBy, authorPropertyMappingDictionary);
        //    }

        //    return PagedList<Author>.Create(collection, authorsResourceParameters.PageNumber, authorsResourceParameters.PageSize);
        //}

        public IEnumerable<AuthorEntity> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
                throw new ArgumentNullException(nameof(authorIds));

            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateAuthor(AuthorEntity author)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
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
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(request.Name.Trim().ToLower()) || x.LastName.ToLower().Contains(request.Name.Trim().ToLower()));
            }

            return query;
        }

        #endregion Private methods
    }
}