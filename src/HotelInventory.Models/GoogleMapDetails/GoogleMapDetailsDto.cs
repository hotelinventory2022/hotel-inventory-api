using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.GoogleMapDetails
{
    public class GoogleMapDetailsDto
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
