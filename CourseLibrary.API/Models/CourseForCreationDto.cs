using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseCustomValidation(ErrorMessage = "Title must be different from description")]
    public class CourseForCreationDto // : IValidatableObject
    {
        [Required(ErrorMessage = "Title Required error msg.")]
        [MaxLength(50, ErrorMessage = "Title length err msg.")]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "Description length err msg.")]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description) yield return
        //            new ValidationResult("The provided description should be different from the title", new[] { "CourseForCreationDto" });
        //}
    }
}