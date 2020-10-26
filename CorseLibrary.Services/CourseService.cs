using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CourseLibrary.Common.Models;
using CourseLibrary.Domain.Entities;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Common.Extensions.Mappings;

namespace CorseLibrary.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseLibraryContext _context;

        public CourseService(ICourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagedList<CourseDto>> GetPagedAsync(Guid authorId, CourseSearchRequest request)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (!AuthorExists(authorId))
                return null;

            var query = _context.Courses.AsNoTracking()
                                 .Where(c => c.AuthorId == authorId)
                                 .AsQueryable();

            query = ApplyFilter(query, request);

            List<CourseDto> list = await query.Select(x => x.ToDto())
                                            .ToListAsync();

            return new PagedList<CourseDto>(list);
        }

        public async Task<CourseDto> GetAuthorsCourseAsync(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (courseId == Guid.Empty)
                throw new ArgumentNullException(nameof(courseId));

            if (!AuthorExists(authorId))
                return null;

            CourseEntity course = await _context.Courses
              .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefaultAsync();

            return course.ToDto();
        }

        public async Task AddCourseAsync(Guid authorId, CreateCourseRequest course)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            if (course == null)
                throw new ArgumentNullException(nameof(course));

            course.AuthorId = authorId;

            _context.Courses.Add(course.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task<CourseDto> UpdateCourseAsync(Guid courseId, CourseUpdateRequest courseRequest)
        {
            if (courseId == Guid.Empty)
                throw new ArgumentNullException(nameof(courseId));

            CourseEntity course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
                return null;

            courseRequest.ToUpdateEntity(ref course);

            await _context.SaveChangesAsync();

            return course.ToDto();
        }

        public async Task DeleteCourseAsync(Guid courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
                throw new ArgumentNullException(nameof(course));

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentNullException(nameof(authorId));

            return _context.Authors.Any(a => a.Id == authorId);
        }

        #region Private methods

        private IQueryable<CourseEntity> ApplyFilter(IQueryable<CourseEntity> query, CourseSearchRequest request)
        {
            if (!string.IsNullOrEmpty(request.Term))
                query = query.Where(x => x.Title.ToLower().Contains(request.Term.Trim().ToLower())
                                    || x.Description.ToLower().Contains(request.Term.Trim().ToLower()));

            return query;
        }

        #endregion Private methods
    }
}