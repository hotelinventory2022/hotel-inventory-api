using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HotelInventory.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
