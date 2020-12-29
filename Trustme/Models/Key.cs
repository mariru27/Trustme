﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class Key
    {
        [Key]
        public int KeyId { set; get; }
        [Required]
        public string certificateName { set; get; }
        public string description { set; get; }
        public int UserId { set; get; }
        public string PublicKey { set; get; }
        public int keySize { set; get; }
        public virtual User User { set; get; }
    }
}
