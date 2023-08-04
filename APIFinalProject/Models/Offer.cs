using System.ComponentModel.DataAnnotations.Schema;

namespace APIFinalProject.Models
{
    public class Offer
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public double Price { get; set; }
        public UnitBuilding UnitBuilding { get; set; }
        public User OwnerOffer { get; set; }
        public User BuyerOffer { get; set; }

    }
}
