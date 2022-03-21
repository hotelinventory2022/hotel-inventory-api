using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Room
{
	public class AvailabiltyRequestModel
	{
		public int OwnerId { get; set; }
		public List<int> PropertyIds { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<AvailabiltyDTO> ListOfAvailabilties { get; set; }
		public int LoggedInUserId { get; set; }
	}
}
