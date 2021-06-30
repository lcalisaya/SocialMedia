using System.Collections.Generic;

namespace SocialMedia.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage? CurrentPage - 1 : (int?)null;
    }
}
