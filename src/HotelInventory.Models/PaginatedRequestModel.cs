using HotelInventory.Models.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models
{
    public class PaginatedRequestModel<T> where T:class
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public T RequestModel{ get; set; }
    }
}