using CourseLibrary.Common.Models.Dtos;
using CourseLibrary.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CourseLibrary.Common.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetCourses(Guid authorId);

        CourseDto GetCourse(Guid authorId, Guid courseId);

        void AddCourse(Guid authorId, CourseEntity course);

        void UpdateCourse(CourseEntity course);

        void DeleteCourse(CourseEntity course);

        bool AuthorExists(Guid authorId);

        bool Save();
    }
}