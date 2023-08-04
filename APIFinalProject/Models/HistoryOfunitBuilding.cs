namespace APIFinalProject.Models
{
    public class HistoryOfunitBuilding
    {
        public int ID { get; set; }
        public string UnitName { get; set; }
        public string CoverImage { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public User User { get; set; }
    }
}

