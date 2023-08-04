using APIFinalProject.Models;

namespace APIFinalProject.DTO
{
    public class OfferDTO
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public double Price { get; set; }
        public int UnitBuildingID { get; set; }
        public string BuyerID { get; set; }
        public string OwnerID { get; set; }
        public string BuyerName { get; set; }
        public string OwnerName { get; set; }
    }
}
