namespace APIFinalProject.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? coverImage { get; set; }
        public virtual List<UnitBuilding> UnitBuildings { get; set; } = new List<UnitBuilding>();
    }
}
