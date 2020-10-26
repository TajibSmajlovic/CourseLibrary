using CourseLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CourseLibrary.Common.Interfaces
{
    public interface ICourseLibraryContext
    {
        DbSet<AuthorEntity> Authors { get; set; }
        DbSet<CourseEntity> Courses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);

        int SaveChanges();
    }
}