using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public class Staff
    {
        [Key, MaxLength(5)]
        public string StaffNo { get; set; } = null!;

        [MaxLength(15)]
        public string FName { get; set; } = null!;

        [MaxLength(15)]
        public string LName { get; set; } = null!;

        [MaxLength(10)]
        public string Position { get; set; } = null!;

        public DateTime Dob { get; set; }

        public decimal Salary { get; set; }

        [MaxLength(4)]
        public string BranchNo { get; set; } = null!;

        [NotMapped]
        public bool IsEven { get; set; }

        // Navigation
        public Branch? Branch { get; set; } = null!;
        public ICollection<PropertyForRent>? Properties { get; set; } = new List<PropertyForRent>();
        public ICollection<Registration>? Registrations { get; set; } = new List<Registration>();
    }
}
