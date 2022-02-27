using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
    [Table("vw_LookupDetails")]
    public class LookupDetailsSnapshot
    {
        #region Constructor
        public LookupDetailsSnapshot()
        {

        }
        #endregion Constructor

        #region Property   
        [Column("LookupTypeParentId")]
        public int LookupTypeParentId { get; set; }
        [Column("LookupTypeParentName")]
        public string LookupTypeParentName { get; set; }
        [Column("LookupTypeId")]
        public int LookupTypeId { get; set; }
        [Column("LookupType")]
        public string LookupType { get; set; }
        [Column("LookupId")]
        public int LookupId { get; set; }
        [Column("Lookup")]
        public string Lookup { get; set; }
        [Column("LookUpDescription")]
        public string LookUpDescription { get; set; }
        #endregion Property
    }
}
