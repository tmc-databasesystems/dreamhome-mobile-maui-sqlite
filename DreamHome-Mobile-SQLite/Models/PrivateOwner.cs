using System.ComponentModel.DataAnnotations;

namespace DreamHome_Mobile_SQLite.Models
{
    public class PrivateOwner
    {
        [Key, MaxLength(5)]
        public string OwnerNo { get; set; } = null!;

        [MaxLength(15)]
        public string FName { get; set; } = null!;

        [MaxLength(15)]
        public string LName { get; set; } = null!;

        [MaxLength(50)]
        public string Address { get; set; } = null!;

        [MaxLength(13)]
        public string TelNo { get; set; } = String.Empty;

        // Navigation
        public ICollection<PropertyForRent> Properties { get; set; } = new List<PropertyForRent>();
    }
}
