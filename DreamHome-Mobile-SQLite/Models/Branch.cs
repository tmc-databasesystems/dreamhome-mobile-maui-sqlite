using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public class Branch
    {
        [Key, MaxLength(4)]
        public string BranchNo { get; set; } = null!;

        [MaxLength(25)]
        public string Street { get; set; } = null!;

        [MaxLength(15)]
        public string City { get; set; } = null!;

        [MaxLength(8)]
        public string Postcode { get; set; } = null!;

        [NotMapped]
        public bool IsEven { get; set; }

        // Navigation
        public ICollection<Staff> Staff { get; set; } = new List<Staff>();
        public ICollection<PropertyForRent> Properties { get; set; } = new List<PropertyForRent>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
