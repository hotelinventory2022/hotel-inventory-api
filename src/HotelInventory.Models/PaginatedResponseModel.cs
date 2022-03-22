using HotelInventory.Models.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models
{
    public class PaginatedResponseModel<T> where T:class
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public T ResponseModel { get; set; }
    }
}
