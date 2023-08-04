using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace APIFinalProject.Models
{
    public class UnitBuilding
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        [MinLength(100)]
        public string Description { get; set; }
        [AllowNull]
        public int? FloorNumber { get; set; }
        public int Area { get; set; }
        public string Governamnet { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public UnitType UnitType { get; set; }
        public PriceType PriceType { get; set; }
        public double Price { get; set; }
        [AllowNull]
        public double? MinPrice { get; set; }
        [AllowNull]
        public double? MaxPrice { get; set; }
        public int CapacityRoom { get; set; }
        public int CapacityBathRoom { get; set; }
        public int Duration { get; set; }
        [AllowNull]
        public DateTime? Date { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string CoverImgage { get; set; }

        public string? UnitImagesPath { get; set; }
        public string? UnitConcreteImagesPath { get; set; }

    }
}
