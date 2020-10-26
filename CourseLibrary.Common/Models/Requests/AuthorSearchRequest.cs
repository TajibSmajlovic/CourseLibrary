namespace CourseLibrary.Common.Models.Requests
{
    public class AuthorSearchRequest : FilterRequest
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}