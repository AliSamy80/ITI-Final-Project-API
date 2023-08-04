using System.ComponentModel.DataAnnotations;

namespace APIFinalProject.DTO
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15,ErrorMessage ="Your Password is limited to {2} to {1} character"),MinLength(6)]
        public string Password { get; set; }
    }
    public class UserDTO : LoginDTO
    {
        [Required]
        public string FullName { get; set; }
       
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }
    }
    public class RegisterDTO:UserDTO
    {
        public string PasswordConfirmed { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
    public class VerifyAccountDTO:UserDTO
    {
        public string PersonalPhoto { get; set; }
        [MaxLength(14), MinLength(14)]
        public string NID { get; set; }
        public string NIDPhoto { get; set; }
        public string CreditNumber { get; set; } = "";
    }
}
