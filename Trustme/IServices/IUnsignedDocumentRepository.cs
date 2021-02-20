using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IUnsignedDocuments
    {
        public UnsignedDocument AddUnsignedDocument(UnsignedDocument unsignedDocument);  
    }
}
