using System;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Common.Models.Dtos
{
    public class CreateCourseRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}