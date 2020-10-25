﻿using System;

namespace CourseLibrary.Common.Models.Dtos
{
    public class AuthorCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }
    }
}