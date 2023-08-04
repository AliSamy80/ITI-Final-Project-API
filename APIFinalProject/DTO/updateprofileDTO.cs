namespace APIFinalProject.DTO
{
    public class UpdateProfileDTO
    {
        public string fullName { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public string personalPhoto { get; set; }
        public string nid { get; set; }
        public string nidPhoto { get; set; }
    }
    public class ProfileDTO : UpdateProfileDTO
    {
        public string email { get; set; }

    }
}
