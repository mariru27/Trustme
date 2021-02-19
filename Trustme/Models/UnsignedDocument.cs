using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class UnsignedDocument
    {
        [Key]
        public int IdUnisignedDocument { get; set; }
        public IFormFile Document { get; set; }
        public string KeyPreference { get; set; }
    }
}
