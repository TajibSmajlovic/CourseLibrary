using CourseLibrary.Common.Models.Dtos;

namespace CourseLibrary.Common.Dtos
{
    public class AuthorDto : BaseDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}