using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class VerifySignModel
    {
        [Required]
        public string Username { get; set; }
        public IEnumerable<Key> Keys { get; set; }

    }
}
