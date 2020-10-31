namespace CourseLibrary.Common.Models.Requests
{
    public class FilterRequest
    {
        private int _pageSize = 10;
        private const int _maxPageSize = 100;

        public string Term { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }
    }
}