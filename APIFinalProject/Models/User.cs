using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace APIFinalProject.Models
{
    public class User:IdentityUser
    {
        [Required,MinLength(3)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Address { get; set; }
        public string PersonalPhoto { get; set; }
        [MaxLength(14),MinLength(14)]

        public string NID { get; set; }
        public string NIDPhoto { get; set; }
        public string CreditNumber { get; set; }
        public virtual List<UnitBuilding> UnitBuildings { get; set; } = new List<UnitBuilding>();


        [InverseProperty("Owner")]
        public ICollection<Meeting> Owner { get; set; }
        [InverseProperty("Buyer")]
        public ICollection<Meeting> Buyer { get; set; }
        [InverseProperty("Representative")]
        public ICollection<Meeting> Representative { get; set; }

        [InverseProperty("OwnerOffer")]
        public ICollection<Offer> OwnerOffer { get; set; }
        [InverseProperty("BuyerOffer")]
        public ICollection<Offer> BuyerOffer { get; set; }

    }
}
