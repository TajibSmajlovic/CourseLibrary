using CourseLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Common.Interfaces
{
    public interface ICourseLibraryContext
    {
        DbSet<AuthorEntity> Authors { get; set; }
        DbSet<CourseEntity> Courses { get; set; }

        int SaveChanges();
    }
}