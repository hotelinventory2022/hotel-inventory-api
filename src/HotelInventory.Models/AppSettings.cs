using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models
{
    public class AppSettings
    {
        public string JwTSecret { get; set; }
        public int JwTExpiration { get; set; }
    }
}
