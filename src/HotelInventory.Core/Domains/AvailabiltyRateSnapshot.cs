using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelInventory.Core.Domains
{
	[Table("Availibilty_Rate")]
	public class AvailabiltyRateSnapshot
	{
		#region constructor
		public AvailabiltyRateSnapshot()
		{

		}
		#endregion
		#region Property
		[Key]
		[Column("Id")]
		public long Id { get; set; }
		[Column("RoomId")]
		public int RoomId { get; set; }
		[Column("StartDate")]
		public DateTime StartDate { get; set; }
		[Column("EndDate")]
		public DateTime EndDate { get; set; }
		[Column("IsFullDayRate")]
		public bool IsFullDayRate { get; set; }
		[Column("Duration_hrs")]
		public int Duration_hrs { get; set; }
		[Column("No_Of_Guests")]
		public int No_Of_Guests { get; set; }
		[Column("Tariff")]
		public int Tariff { get; set; }
		[Column("IsActive")]
		public bool IsActive { get; set; }
		[Column("IsDeleted")]
		public bool IsDeleted { get; set; }
		[Column("CreatedBy")]
		public int CreatedBy { get; set; }
		[Column("CreatedOn")]
		public DateTime CreatedOn { get; set; }
		[Column("LastUpdatedBy")]
		public int LastUpdatedBy { get; set; }
		[Column("LastUpdatedOn")]
		public DateTime LastUpdatedOn { get; set; }
		#endregion
	}
}
