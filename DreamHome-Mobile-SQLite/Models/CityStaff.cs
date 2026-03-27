
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public sealed class CityStaff
    {
        public string StaffNo { get; set; } = null!;

        public string FName { get; set; } = null!;

        public string LName { get; set; } = null!;

        public string Position { get; set; } = null!;

        [NotMapped]
        public bool IsEven { get; set; }
    }
}
