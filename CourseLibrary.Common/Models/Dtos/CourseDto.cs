using System;

namespace CourseLibrary.Common.Models.Dtos
{
    public class CourseDto : BaseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }
    }
}