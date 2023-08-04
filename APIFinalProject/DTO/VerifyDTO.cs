using System.ComponentModel.DataAnnotations;

namespace APIFinalProject.DTO
{
    public class VerifyDTO
    {
        public IFormFile PersonalPhoto { get; set; }
        [MaxLength(14), MinLength(14)]
        public string NID { get; set; }
        public IFormFile NIDPhoto { get; set; }
    }
}
