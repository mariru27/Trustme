using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class SignedDocumentsViewModel
    {

        public int IdSignedDocument { get; set; }
        public string Name { get; set; }
        public string SignedByUsername { get; set; }
        public string Signature { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
    }
}
