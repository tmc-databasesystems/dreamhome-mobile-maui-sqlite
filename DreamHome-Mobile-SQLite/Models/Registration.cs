using System.ComponentModel.DataAnnotations;

namespace DreamHome_Mobile_SQLite.Models
{
    public class Registration
    {
        // Composite key: ClientNo + BranchNo (configured in OnModelCreating)
        [MaxLength(5)]
        public string ClientNo { get; set; } = null!;

        [MaxLength(4)]
        public string BranchNo { get; set; } = null!;

        [MaxLength(5)]
        public string StaffNo { get; set; } = null!;

        public DateTime DateJoined { get; set; }

        // Navigation
        public Client Client { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
        public Staff Staff { get; set; } = null!;
    }
}
