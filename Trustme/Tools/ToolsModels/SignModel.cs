using Org.BouncyCastle.Crypto;
using System.IO;

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
