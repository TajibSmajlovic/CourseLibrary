using CourseLibrary.Common.Extensions;
using System.Collections.Generic;

namespace CourseLibrary.Common.Models
{
    public class PagedList<T>
    {
        public PagedList(List<T> items)
        {
            Count = items.Count;
            Data = items;
        }

        public int Count { get; private set; }
        public IList<T> Data { get; private set; }
    }
}