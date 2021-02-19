﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class UserSignedDocument
    {
        [Key]
        public int IdUserSignedDocument { get; set; }
        public int UserId { get; set; }
        public int SignedDocumentId { get; set; }
    }
}
