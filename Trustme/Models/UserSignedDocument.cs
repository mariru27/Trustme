using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class UserSignedDocument
    {
        [Key]
        public int IdUserSignedDocument { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SignedDocumentId { get; set; }
        public SignedDocument SignedDocument { get; set; }
    }
}
