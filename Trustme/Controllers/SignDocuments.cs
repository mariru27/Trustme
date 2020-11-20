using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Trustme.Controllers
{
    //[Authorize]
    public class SignDocuments : Controller
    {
        public Administration admin;
        private const string SignatureAlgorithm = "sha1WithRSA";

        public SignDocuments(Administration _admin)
        {
            admin = _admin;
        }
        public IActionResult Index()
        {
            return View();
        }

        public string test()
        {
            //this do not work, checkAuthentification return a null reference
            bool a = admin.checkAuthentification();
            if (a == false)
                return "nu este logat";
            return "este logat";
        }

        //[HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Signdocument()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public string GenerateCertificate()
        {

            // Keypair Generator
            RsaKeyPairGenerator kpGenerator = new RsaKeyPairGenerator();
            kpGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));

            // Create a keypair
            AsymmetricCipherKeyPair kp = kpGenerator.GenerateKeyPair();

            // Certificate Generator
            X509V3CertificateGenerator cGenerator = new X509V3CertificateGenerator();
            cGenerator.SetSerialNumber(BigInteger.ProbablePrime(120, new Random()));
            cGenerator.SetSubjectDN(new X509Name("CN=" + "trustme.com"));
            cGenerator.SetIssuerDN(new X509Name("CN=" + "Trustme Application"));
            cGenerator.SetNotBefore(DateTime.Now);
            cGenerator.SetNotAfter(DateTime.Now.Add(new TimeSpan(365, 0, 0, 0))); // Expire in 1 year
            cGenerator.SetSignatureAlgorithm(SignatureAlgorithm); // See the Appendix Below for info on the hash types supported by Bouncy Castle C#
            cGenerator.SetPublicKey(kp.Public); // Only the public key should be used here!

            var cert = cGenerator.Generate(kp.Private); // Create a self-signed cert

            byte[] encoded = cert.GetEncoded();

            using (FileStream outStream = new FileStream("C:\\Users\\Marina Rusu\\Desktop\\Trustme\\Trustme\\Trustme\\Certificates\\cetificate.der", FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(encoded, 0, encoded.Length);
            }

            PrivateKeyInfo pkInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(kp.Private);
            string privatekey = Convert.ToBase64String(pkInfo.GetDerEncoded());

            byte[] privatekey_byte = Encoding.ASCII.GetBytes(privatekey);

            using (FileStream outStream = new FileStream("C:\\Users\\Marina Rusu\\Desktop\\Trustme\\Trustme\\Trustme\\Certificates\\privatekey.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(privatekey_byte, 0, privatekey_byte.Length);
            }
            return privatekey;
        }

    }
}
