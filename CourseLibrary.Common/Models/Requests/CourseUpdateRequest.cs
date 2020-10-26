using System;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Common.Models.Requests
{
    public class CourseUpdateRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
    }
}