using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Tools.ToolsModels
{
    public class SignModel
    {
        public AsymmetricKeyParameter privatekeyy { set; get; }
        public byte[] fileBytesdoc { set; get; }
        public StreamReader reader { set; get; }
        public string keypath { set; get; }
        public bool verifytest { set; get; }
        public bool validKey { set; get; }
    }
}
