namespace ApiTemplate.ViewModels.SystemSettingGroupViewModels
{ 
    using System.Collections.Generic;

    public class PageResult<T>
        where T : class
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public bool HasNextPage => Page < TotalPages;

        public bool HasPreviousPage => Page > 1;

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public List<T> Items { get; set; }
    }
}