using APIFinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APIFinalProject.AhmedFavoritesDTO
{
    public class GetAllFavoUnitsDTO
    {
        public string UnitName { get; set; }
        [MinLength(100)]
        public string UnitDescription { get; set; }
        [AllowNull]
        public int UnitFloorNumber { get; set; }
        public string UnitGovernamnet { get; set; }
        public string UnitCity { get; set; }
        public string UnitAddress { get; set; }
        public string UnitLocation { get; set; }

        public UnitType UnitType { get; set; }

        public double Price { get; set; }

        public string CoverImage { get; set; }

        public UnitBuilding unitDTO { get; set;  }  ///
        public User Owner { get; set; }
       
        public Category Category { get; set; }



    }
}
