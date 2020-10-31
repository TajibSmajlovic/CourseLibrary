using CourseLibrary.Common.Constants;
using CourseLibrary.Common.Enums;

namespace CourseLibrary.Common.Models.Requests
{
    public class AuthorSearchRequest : FilterRequest
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string MainCategory { get; set; }
        public string Fields { get; set; }
        public string OrderBy { get; set; } = AuthorOrderBy.NAME;
        public SortOrder SortOrder { get; set; }
    }
}