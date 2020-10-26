using System;
using System.Threading.Tasks;
using CourseLibrary.Common.Models;
using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Common.Models.Requests;

namespace CourseLibrary.Common.Interfaces
{
    public interface ICourseService
    {
        Task<PagedList<CourseDto>> GetPagedAsync(Guid authorId, CourseSearchRequest request);

        Task<CourseDto> GetAuthorsCourseAsync(Guid authorId, Guid courseId);

        Task AddCourseAsync(Guid AuthorId, CreateCourseRequest course);

        Task<CourseDto> UpdateCourseAsync(Guid courseId, CourseUpdateRequest courseRequest);

        Task DeleteCourseAsync(Guid courseId);

        bool AuthorExists(Guid authorId);
    }
}