﻿namespace SmartLibrary.Core.Data
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
