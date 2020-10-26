using System;

namespace CourseLibrary.Common.Models.Requests
{
    public class AuthorUpdateRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }
    }
}