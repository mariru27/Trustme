﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class KeysUnsignedDocumentViewModel
    {
        public SelectList Keys { get; set; }
        public UnsignedDocument UnsignedDocument { get; set; }
        public int KeyId;

    }
}
