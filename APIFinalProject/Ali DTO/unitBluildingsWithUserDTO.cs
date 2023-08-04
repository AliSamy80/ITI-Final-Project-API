using APIFinalProject.Models;

namespace APIFinalProject.Ali_DTO
{
    public class unitBluildingsWithUserDTO
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public string CoverImageUnit { get; set; }
        //public DateTime Date { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
    }
}
