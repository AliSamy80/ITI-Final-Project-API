using APIFinalProject.Models;

namespace APIFinalProject.DTO
{
    public class MenuPriceDTO
    {
        public int NumberOfDays { get; set; }
        public double Price { get; set; }
        public UnitType unitType { get; set; }

        
    }
}
