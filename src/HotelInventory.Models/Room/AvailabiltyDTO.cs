using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Room
{
	public class AvailabiltyDTO
    {
		public long Id { get; set; }
		public int RoomId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsFullDayRate { get; set; }
		public int Duration_hrs { get; set; }
		public int No_Of_Guests { get; set; }
		public int Tariff { get; set; }
	}
}
