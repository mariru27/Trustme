using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;

namespace Trustme.Tools.ToolsModels
{
    public class KeyPairCertificateGeneratorModel
    {
        public X509V3CertificateGenerator CertificateGenerator { get; set; }
        public AsymmetricCipherKeyPair KeyPair { get; set; }
        
    }
}
