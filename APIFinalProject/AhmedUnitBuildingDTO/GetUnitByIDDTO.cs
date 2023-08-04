using APIFinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APIFinalProject.AhmedUnitBuildingDTO
{
    public class GetUnitByIDDTO
    {
        public string Name { get; set; }
        [MinLength(100)]
        public string Description { get; set; }
        [AllowNull]
        public int FloorNumber { get; set; }
        public string ContractImages { get; set; }
        public string Governamnet { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

        public string UnitType { get; set; }
        public PriceType PriceType { get; set; }
        public double Price { get; set; }
        [AllowNull]
        public double MinPrice { get; set; }
        [AllowNull]
        public double MaxPrice { get; set; }
        public int Area { get; set; }
        public int CapacityRoom { get; set; }
        public int CapacityBathRoom { get; set; }
        public int Duration { get; set; }
        [AllowNull]
        public DateTime Date { get; set; }
        public string CoverImage { get; set; }
        public string UnitImages { get; set; }
        public User Owner { get; set; }
        public Category Category { get; set; }
    }
}
