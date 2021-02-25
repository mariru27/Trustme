using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Trustme.Models;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using Org.BouncyCastle.OpenSsl;
using System.Text;
using System.Net.Http.Headers;
using AppContext = Trustme.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Trustme.IServices;
using Trustme.Service;
using Trustme.ViewModels;
using Trustme.ITools;
using Trustme.Tools.ToolsModels;

namespace Trustme.Tools
{
    public class Certificate : ICertificate
    {
        private const string SignatureAlgorithm = "sha1WithRSA";
        private IHttpRequestFunctions _HttpRequestFunctions;
        private IKeyRepository _KeyRepository;
        public Certificate(IKeyRepository keyRepository)
        {
            _KeyRepository = keyRepository;
        }
        public void CrateAndStoreKeyUserInDB(User currentUser, KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel, Key key)
        {


            TextWriter textWriter1 = new StringWriter();
            PemWriter pemWriter1 = new PemWriter(textWriter1);
            pemWriter1.WriteObject(keyPairCertificateGeneratorModel.KeyPair.Public);
            pemWriter1.Writer.Flush();

            string publicKey = textWriter1.ToString();

            Key currentKey = new Key();
            currentKey.CertificateName = key.CertificateName;
            currentKey.Description = key.Description;
            currentKey.KeySize = key.KeySize;
            currentKey.PublicKey = publicKey;


            UserKeyModel userKeyModel = new UserKeyModel();
            userKeyModel.User = currentUser;
            userKeyModel.Key = currentKey;

            _KeyRepository.AddKey(userKeyModel);

        }

        public KeyPairCertificateGeneratorModel GenereateCertificate(int keySize)
        {
            // Keypair Generator
            RsaKeyPairGenerator kpGenerator = new RsaKeyPairGenerator();
            kpGenerator.Init(new KeyGenerationParameters(new SecureRandom(), keySize));

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

            KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel = new KeyPairCertificateGeneratorModel
            {
                CertificateGenerator = cGenerator,
                KeyPair = kp
            };
        
            return keyPairCertificateGeneratorModel;
        }
    }
}
