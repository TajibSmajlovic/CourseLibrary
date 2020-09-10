using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLibrary.Common.Models.Requests
{
    public class AuthorSearchRequest
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}