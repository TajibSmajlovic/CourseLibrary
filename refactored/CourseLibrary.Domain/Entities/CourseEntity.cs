using System;

namespace CourseLibrary.Domain.Entities
{
    public class CourseEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public AuthorEntity Author { get; set; }

        public Guid AuthorId { get; set; }
    }
}