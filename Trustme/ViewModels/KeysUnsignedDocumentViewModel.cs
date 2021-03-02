﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class KeysUnsignedDocumentViewModel
    {
        public IEnumerable<Key> Keys { get; set; }
        public Key Key { get; set; }
        public UnsignedDocument UnsignedDocument { get; set; }
        public IFormFile PkFile { get; set; }

    }
}
