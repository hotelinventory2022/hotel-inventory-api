using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.Models.Property
{
    public class PropertySearchRequestModel
    {
        public int OwnerId { get; set; }
        public int Country_State_City_Area_Id { get; set; }
        public int PropertyTypeId { get; set; }
        public int No_of_Rooms { get; set; }
        public int No_Of_Adult { get; set; }
        public int No_Of_Child { get; set; }
        public int LowerBudgetRange { get; set; }
        public int UpperBudgetRange { get; set; }
        public DateTime Check_In_Date { get; set; }
        public DateTime Check_Out_Date { get; set; }
        public List<int> PropertyFacilities { get; set; }
        public List<int> RoomFacilities { get; set; }
        public string Rating { get; set; }
    }
}
