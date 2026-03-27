using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public class PropertyForRent
    {
        [Key, MaxLength(5)]
        public string PropertyNo { get; set; } = null!;

        [MaxLength(25)]
        public string Street { get; set; } = null!;

        [MaxLength(15)]
        public string City { get; set; } = null!;

        [MaxLength(8)]
        public string Postcode { get; set; } = null!;

        [MaxLength(10)]
        public string Type { get; set; } = null!;

        public byte Rooms { get; set; } // NUMBER(1) -> TINYINT -> byte

        public decimal Rent { get; set; } // DECIMAL(5,1)

        [MaxLength(5)]
        public string OwnerNo { get; set; } = null!;

        [MaxLength(5)]
        public string? StaffNo { get; set; }

        [MaxLength(4)]
        public string BranchNo { get; set; } = null!;

        [NotMapped]
        public bool IsEven { get; set; }

        // Navigation
        public PrivateOwner Owner { get; set; } = null!;
        public Staff? Staff { get; set; }
        public Branch Branch { get; set; } = null!;
        public ICollection<Viewing> Viewings { get; set; } = new List<Viewing>();
    }
}
