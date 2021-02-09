using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
            this.UserKeyId = key.UserKeyId;
            this.PublicKey = key.PublicKey;
            this.KeySize = key.KeySize;
            this.UserKey = key.UserKey;

        }
        [Key]
        public int KeyId { set; get; }
        [Required]
        public string CertificateName { set; get; }
        public string Description { set; get; }
        public int UserKeyId { set; get; }
        public string PublicKey { set; get; }
        public int KeySize { set; get; }
        public UserKey UserKey { set; get; }
    }
}
