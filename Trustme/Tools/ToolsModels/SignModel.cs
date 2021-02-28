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
        AsymmetricKeyParameter privatekeyy;
        byte[] fileBytesdoc;
        StreamReader reader;
        string keypath;
    }
}
