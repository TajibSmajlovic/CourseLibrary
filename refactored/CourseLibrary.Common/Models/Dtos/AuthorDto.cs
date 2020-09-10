using CourseLibrary.Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLibrary.Common.Dtos
{
    public class AuthorDto : BaseDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}