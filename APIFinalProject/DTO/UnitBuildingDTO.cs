using APIFinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APIFinalProject.DTO
{
    public class UnitBuildingCardDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public string Governamnet { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public UnitType UnitType { get; set; }
        public double Price { get; set; }
        public int CapacityRoom { get; set; }
        public int CapacityBathRoom { get; set; }
        public IFormFile? CoverImage { get; set; }
        public string? CoverImageString { get; set; }
    }
    public class UnitBuildingDetailsDTO : UnitBuildingCardDTO
    {

        [MinLength(100)]
        public string Description { get; set; }
        public int? FloorNumber { get; set; }
        public string Address { get; set; }
        public PriceType PriceType { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        [AllowNull]
        public int CategoryId { get; set; }
        public List<IFormFile> UnitImagesFile { get; set; } = new List<IFormFile>();
        public string UnitImagesString { get; set; }
        public string OwnerID { get; set; }

    }

    public class UnitBuildingDTO : UnitBuildingDetailsDTO
    {
        public List<IFormFile> UnitConcreteImagesFile { get; set; } = new List<IFormFile>();

    }

    public class UnitBuildingUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int CapacityRoom { get; set; }
        public int CapacityBathRoom { get; set; }
        public PriceType PriceType { get; set; }
    }

    public class UpdateDuration{
        public int ID { get; set; }
        public int Duration { get; set; }
        public UnitType UnitType { get; set; }
    }
    class UpdateDate
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
    }
    public class CityCountDTO {
        public int Count { get; set; }
        public string CityName { get; set; }
        public string CityImage { get; set; }

    }
}
