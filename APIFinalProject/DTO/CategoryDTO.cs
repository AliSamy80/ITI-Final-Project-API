namespace APIFinalProject.DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? coverImage { get; set; }
        public IFormFile CoverImageFile { get; set; }
    }
    public class CategoryWithBuildinDTO:CategoryDTO
    {
        public List<UnitBuildingCardDTO> UnitBuildingCardDTOs { get; set; } = new List<UnitBuildingCardDTO>();
    }
}
