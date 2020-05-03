using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseCustomValidation(ErrorMessage = "Title must be different from description")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "Title Required error msg.")]
        [MaxLength(50, ErrorMessage = "Title length err msg.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [MaxLength(250, ErrorMessage = "Description length err msg.")]
        public virtual string Description { get; set; }
    }
}