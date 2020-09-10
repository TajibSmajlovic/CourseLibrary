using CourseLibrary.Common.Interfaces;
using CourseLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CourseLibrary.Database
{
    public class CourseLibraryContext : DbContext, ICourseLibraryContext
    {
        public CourseLibraryContext(DbContextOptions<CourseLibraryContext> options)
             : base(options)
        {
        }

        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureAuthor(modelBuilder);
            ConfigureCourse(modelBuilder);

            Data.Seed(modelBuilder);
        }

        #region Private methods

        private void Audit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity))
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(BaseEntity.IsDeleted)] = true;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues[nameof(BaseEntity.UpdatedAt)] = DateTime.UtcNow;
                        break;

                    case EntityState.Added:
                        entry.CurrentValues[nameof(BaseEntity.CreatedAt)] = DateTime.UtcNow;
                        entry.CurrentValues[nameof(BaseEntity.UpdatedAt)] = DateTime.UtcNow;
                        break;

                    default:
                        break;
                }
            }
        }

        private void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorEntity>().HasQueryFilter(x => !x.IsDeleted);
        }

        private void ConfigureCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseEntity>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<CourseEntity>().HasQueryFilter(x => !x.IsDeleted);
        }

        #endregion Private methods
    }
}