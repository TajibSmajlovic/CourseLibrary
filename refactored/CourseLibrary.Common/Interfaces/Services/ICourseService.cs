using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;
using CourseLibrary.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CourseLibrary.Common.Interfaces
{
    public interface ICourseService
    {
        Task<PagedList<CourseDto>> GetPagedAsync(Guid authorId, CourseSearchRequest request);

        Task<CourseDto> GetAuthorsCourseAsync(Guid authorId, Guid courseId);

        Task AddCourseAsync(Guid AuthorId, CourseCreationDto course);

        void UpdateCourse(CourseEntity course);

        void DeleteCourse(CourseEntity course);

        bool AuthorExists(Guid authorId);
    }
}