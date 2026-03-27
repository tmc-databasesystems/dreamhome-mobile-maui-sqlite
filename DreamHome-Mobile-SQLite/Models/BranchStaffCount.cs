using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public sealed class BranchStaffCount
    {
        public string BranchNo { get; set; } = null!;
        public string StaffNo { get; set; } = null!;
        public int MyCount { get; set; }

        [NotMapped]
        public bool IsEven { get; set; }
    }
}
