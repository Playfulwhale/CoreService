namespace ApiTemplate.ViewModels.PackageSubscriberViewModels
{
    using System.Collections.Generic;

    public class PageResult<T>
        where T : class
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public bool HasNextPage { get => Page < TotalPages; }

        public bool HasPreviousPage { get => Page > 1; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public List<T> Items { get; set; }
    }
}
