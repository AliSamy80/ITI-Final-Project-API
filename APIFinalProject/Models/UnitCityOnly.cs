using Microsoft.EntityFrameworkCore;

namespace APIFinalProject.Models
{

    public class UnitCityOnly
    {
        public int ID { get; set; }
        public int cityCount { get; set; }
        public string CityName { get; set; }
        public string? imagepath { get; set; }
    }
}
