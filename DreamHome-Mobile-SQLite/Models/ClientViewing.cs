using System.ComponentModel.DataAnnotations.Schema;

namespace DreamHome_Mobile_SQLite.Models
{
    public sealed class ClientViewing
    {
        public string ClientNo { get; set; } = null!;
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string PropertyNo { get; set; } = null!;
        public string? Comment { get; set; }

        [NotMapped]
        public bool IsEven { get; set; }
    }
}
