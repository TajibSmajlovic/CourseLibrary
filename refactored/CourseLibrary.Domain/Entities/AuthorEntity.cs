using CourseLibrary.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CourseLibrary.Domain.Entities
{
    public class AuthorEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }

        public ICollection<CourseEntity> Courses { get; set; }
            = new List<CourseEntity>();
    }
}