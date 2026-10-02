namespace RealState.Dal.Models
{
    public class RentVillaDto
    {
    
        public string Title { get; set; }
        public string Description { get; set; }
        public int FirstMoney { get; set; }
        public int MoneyRent { get; set; }
        public int FloorArea { get; set; }
        public DateTime DateOfBuild { get; set; }
        public string City { get; set; }
        public string province { get; set; }
        public string neighborhood { get; set; }
        public string Servis { get; set; }

        public bool Elevator { get; set; }
        public int RoomCount { get; set; }
        public bool Parking { get; set; }
        public bool  warehouse {get; set;}
        public string Code { get; set; }

    }
}
