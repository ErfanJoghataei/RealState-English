using RealState.Dal.Entities;

namespace RealState.UI.Models;

public static class ShowcaseProperties
{
    // Negative IDs keep the portfolio listings separate from database records.
    public static readonly IReadOnlyList<Properties> All = new List<Properties>
    {
        new() { Id = -1, Code = "RS-101", Title = "Sea View Residence", Description = "A bright coastal home with generous living space, a private terrace, and easy access to the waterfront.", Price = 1850000, FloorArea = 2583, DateOfBuild = new DateTime(2021, 1, 1), City = "Malibu", province = "California", neighborhood = "Point Dume", RoomCount = 4, BathroomCount = "3", ToiletCount = "3", Category = "Villa", Servis = "Sale", Parking = true, Elevator = false, warehouse = true, SecuritySystem = "CCTV", YardArea = "1938", ImageUrlMain = "/images/05-2.jpg" },
        new() { Id = -2, Code = "RS-102", Title = "Garden Apartment", Description = "A calm city apartment with leafy views, modern finishes, and a practical open plan layout.", Price = 890000, FloorArea = 1668, DateOfBuild = new DateTime(2022, 1, 1), City = "New York", province = "New York", neighborhood = "Brooklyn Heights", RoomCount = 3, BathroomCount = "2", ToiletCount = "2", Category = "Apartment", Servis = "Sale", Parking = true, Elevator = true, warehouse = true, SecuritySystem = "Concierge", YardArea = "0", ImageUrlMain = "/images/download.jfif" },
        new() { Id = -3, Code = "RS-103", Title = "Northern Hills Villa", Description = "A contemporary retreat with a broad balcony, mountain outlook, and quiet landscaped surroundings.", Price = 2050000, FloorArea = 3337, DateOfBuild = new DateTime(2020, 1, 1), City = "Aspen", province = "Colorado", neighborhood = "Red Mountain", RoomCount = 5, BathroomCount = "4", ToiletCount = "4", Category = "Villa", Servis = "Sale", Parking = true, Elevator = false, warehouse = true, SecuritySystem = "Alarm", YardArea = "2799", ImageUrlMain = "/images/northern-hills-villa.jpg" },
        new() { Id = -4, Code = "RS-104", Title = "Modern City Loft", Description = "An efficient modern home close to shops, restaurants, and transit with a flexible living area.", Price = 620000, FloorArea = 1206, DateOfBuild = new DateTime(2023, 1, 1), City = "Chicago", province = "Illinois", neighborhood = "West Loop", RoomCount = 2, BathroomCount = "2", ToiletCount = "2", Category = "Apartment", Servis = "Sale", Parking = true, Elevator = true, warehouse = false, SecuritySystem = "Entry system", YardArea = "0", ImageUrlMain = "/images/download (1).jfif" }
    };

    public static Properties ToDatabaseCopy(Properties p) => new()
    {
        Title = p.Title,
        Description = p.Description,
        Price = p.Price,
        FirstMoney = p.FirstMoney,
        MoneyRent = p.MoneyRent,
        FloorArea = p.FloorArea,
        DateOfBuild = p.DateOfBuild,
        City = p.City,
        province = p.province,
        neighborhood = p.neighborhood,
        Servis = p.Servis,
        TheFloor = p.TheFloor,
        Elevator = p.Elevator,
        RoomCount = p.RoomCount,
        Parking = p.Parking,
        warehouse = p.warehouse,
        Code = p.Code,
        Category = p.Category,
        BathroomCount = p.BathroomCount,
        ImageUrlMain = p.ImageUrlMain,
        ImageUrl1 = p.ImageUrl1,
        ImageUrl2 = p.ImageUrl2,
        ImageUrl3 = p.ImageUrl3,
        ImageUrl4 = p.ImageUrl4,
        SecuritySystem = p.SecuritySystem,
        ToiletCount = p.ToiletCount,
        YardArea = p.YardArea
    };
}

