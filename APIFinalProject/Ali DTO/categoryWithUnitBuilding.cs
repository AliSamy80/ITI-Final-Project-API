namespace APIFinalProject.Ali_DTO
{
    public class categoryWithUnitBuilding
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<string> UnitBuildingsNames { get; set; } = new List<string>();

    }
}
