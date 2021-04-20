using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UnsignedDocumentUserKey
    {
        public User User { get; set; } = new User();
        public Key Key { get; set; } = new Key();
        public UnsignedDocument UnsignedDocument { get; set; } = new UnsignedDocument();
    }
}
