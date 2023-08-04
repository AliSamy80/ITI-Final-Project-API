using System.ComponentModel.DataAnnotations.Schema;

namespace APIFinalProject.Models
{
    public class Meeting
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public UnitBuilding UnitBuilding { get; set; }
        public User Owner { get; set; }
        public User Buyer { get; set; }
        public User Representative { get; set; }
    }
}
