using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Lookup
{
    public class LookupDetailsDTO
    {
        #region Constructor
        public LookupDetailsDTO()
        {

        }
        #endregion Constructor

        #region Property
        public int LookupTypeId { get; set; }
        public string LookupType { get; set; }
        public int LookupId { get; set; }
        public string Lookup { get; set; }
        public string LookUpDescription { get; set; }
        #endregion Property
    }
}
