using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class Key
    {
        public Key() { }
        public Key(Key key)
        {

            this.KeyId = key.KeyId;
            this.CertificateName = key.CertificateName;
            this.Description = key.Description;
            this.PublicKey = key.PublicKey;
            this.KeySize = key.KeySize;
            this.UserKey = key.UserKey;

        }
        [Key]
        public int KeyId { set; get; }

        [Required]
        [Display(Name = "Certificate Name")]
        public string CertificateName { set; get; }
        public string Description { set; get; }

        public string PublicKey { set; get; }
        [Required]
        public int KeySize { set; get; }
        public UserKey UserKey { set; get; }

    }
}
