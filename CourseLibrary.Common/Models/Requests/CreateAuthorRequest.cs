using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Common.Models.Dtos
{
    public class CreateAuthorRequest : BaseDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        public string MainCategory { get; set; }

        public ICollection<CreateCourseRequest> Courses { get; set; } = new List<CreateCourseRequest>();
    }
}