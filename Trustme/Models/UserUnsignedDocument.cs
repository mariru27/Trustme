using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class UserUnsignedDocument
    {
        [Key]
        public int IdUserUnsignedDocument { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UnsignedDocumentId { get; set; }
        public UnsignedDocument UnsignedDocument { get; set; }
    }
}
