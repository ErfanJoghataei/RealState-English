using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RealState.Dal.Entities
{
    public class Properties
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }

        public int? FirstMoney { get; set; }
        public int? MoneyRent { get; set; }
        public int FloorArea { get; set; }
        public DateTime DateOfBuild { get; set; }
        public string City { get; set; }
        public string province { get; set; }
        public string neighborhood { get; set; }
        public string Servis { get; set; }
        public int? TheFloor { get; set; }
        public bool Elevator { get; set; }
        public int RoomCount { get; set; }
        public bool Parking { get; set; }
        public bool warehouse { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string BathroomCount { get; set; }
        public string ImageUrlMain { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }
        public string SecuritySystem { get; set; }
        public string ToiletCount { get; set; }
        public string? YardArea { get; set; }
    }

}
