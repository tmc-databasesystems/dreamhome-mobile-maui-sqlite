using System.ComponentModel.DataAnnotations;

namespace DreamHome_Mobile_SQLite.Models
{
    public class Client
    {
        [Key, MaxLength(5)]
        public string ClientNo { get; set; } = null!;

        [MaxLength(15)]
        public string FName { get; set; } = null!;

        [MaxLength(15)]
        public string LName { get; set; } = null!;

        [MaxLength(13)]
        public string TelNo { get; set; } = null!;

        [MaxLength(10)]
        public string PrefType { get; set; } = null!;

        public decimal MaxRent { get; set; } // DECIMAL(5,1)

        // Navigation
        public ICollection<Viewing> Viewings { get; set; } = new List<Viewing>();
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
