using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public class Viewing
    {
        // Composite key: PropertyNo + ClientNo (configured in OnModelCreating)
        [MaxLength(5)]
        public string ClientNo { get; set; } = null!;

        [MaxLength(5)]
        public string PropertyNo { get; set; } = null!;

        public DateTime ViewDate { get; set; }

        [MaxLength(50)]
        public string? Comments { get; set; }

        [NotMapped]
        public bool IsEven { get; set; }

        // Navigation
        public Client Client { get; set; } = null!;
        public PropertyForRent Property { get; set; } = null!;
    }
}
