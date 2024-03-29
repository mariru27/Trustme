﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Models;
using Trustme.Tools.ToolsModels;
using Trustme.ViewModels;

namespace Trustme.Tools
{
    public class Certificate : ICertificate
    {
        private const string SignatureAlgorithm = "sha1WithRSA";
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private readonly IKeyRepository _KeyRepository;
        [Obsolete]
        private readonly IHostingEnvironment Environment;

        [Obsolete]
        public Certificate(IKeyRepository keyRepository, IHostingEnvironment environment, IHttpRequestFunctions httpRequestFunctions)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _KeyRepository = keyRepository;
            Environment = environment;

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

        [Obsolete]
        public FileContentResult CreateCertificateFileAndPrivateKeyFile(KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel, string certificateName, HttpContext httpContext)
        {
            string wwwPath = this.Environment.WebRootPath;

            X509Certificate cert = keyPairCertificateGeneratorModel.CertificateGenerator.Generate(keyPairCertificateGeneratorModel.KeyPair.Private); // Create a self-signed cert

            byte[] encoded = cert.GetEncoded();

            string pathDir = Path.Combine(wwwPath, "Certificate_PKey");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }


            string pathCertificate = Path.Combine(pathDir, "certificate.der");
            using (FileStream outStream = new FileStream(pathCertificate, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(encoded, 0, encoded.Length);
            }

            PrivateKeyInfo pkInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPairCertificateGeneratorModel.KeyPair.Private);
            string privatekey = Convert.ToBase64String(pkInfo.GetDerEncoded());


            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(keyPairCertificateGeneratorModel.KeyPair.Private);
            pemWriter.Writer.Flush();

            string privateKey = textWriter.ToString();

            byte[] privatekey_byte = Encoding.ASCII.GetBytes(privateKey);

            string pathPrivateKey = Path.Combine(pathDir, "privateKey.pem");
            using (FileStream outStream = new FileStream(pathPrivateKey, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(privatekey_byte, 0, privatekey_byte.Length);
            }

            string pathDirectoryZip = Path.Combine(wwwPath, "Certificate_Key");

            ZipFile.CreateFromDirectory(pathDir, pathDirectoryZip, System.IO.Compression.CompressionLevel.Optimal, false);

            const string contentType = "application/zip";

            httpContext.Response.ContentType = contentType;
            var result = new FileContentResult(System.IO.File.ReadAllBytes(pathDirectoryZip), contentType);


            System.IO.DirectoryInfo dir = new DirectoryInfo(pathDir);
            foreach (FileInfo files in dir.GetFiles())
            {
                files.Delete();
            }
            Directory.Delete(pathDir);
            System.IO.File.Delete(pathDirectoryZip);

            result.FileDownloadName = certificateName + ".zip";
            return result;
        }

        [Obsolete]
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

        [Obsolete]
        public FileContentResult DoAllGenereateSaveInDBCreateCertificateAndPKFile(User currentUser, Key key, HttpContext httpContext)
        {

            KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel = GenereateCertificate(key.KeySize);
            CrateAndStoreKeyUserInDB(currentUser, keyPairCertificateGeneratorModel, key);
            return CreateCertificateFileAndPrivateKeyFile(keyPairCertificateGeneratorModel, key.CertificateName, httpContext);
        }

        [Obsolete]
        public FileContentResult GenerateCertificatePivateKey(string certificateName, string description, int keySize, HttpContext httpContext)
        {
            User currentUser = _HttpRequestFunctions.GetUser(httpContext);

            string wwwPath = this.Environment.WebRootPath;

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
            cGenerator.SetPublicKey(kp.Public); // Only the public key should be used here!
            //we saved public key in database
            if (_HttpRequestFunctions.IsloggedIn(httpContext) == true)
            {

                //string username = _HttpRequestFunctions.GetUsername(HttpContext);


                TextWriter textWriter1 = new StringWriter();
                PemWriter pemWriter1 = new PemWriter(textWriter1);
                pemWriter1.WriteObject(kp.Public);
                pemWriter1.Writer.Flush();

                string publicKey = textWriter1.ToString();

                Key currentKey = new Key();
                currentKey.CertificateName = certificateName;
                currentKey.Description = description;
                currentKey.KeySize = keySize;
                currentKey.PublicKey = publicKey;


                UserKeyModel userKeyModel = new UserKeyModel();
                userKeyModel.User = currentUser;
                userKeyModel.Key = currentKey;

                _KeyRepository.AddKey(userKeyModel);

            }

            var cert = cGenerator.Generate(kp.Private); // Create a self-signed cert

            byte[] encoded = cert.GetEncoded();

            string pathDir = Path.Combine(wwwPath, "Certificate_PKey");
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }


            string pathCertificate = Path.Combine(pathDir, "certificate.der");
            using (FileStream outStream = new FileStream(pathCertificate, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(encoded, 0, encoded.Length);
            }

            PrivateKeyInfo pkInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(kp.Private);
            string privatekey = Convert.ToBase64String(pkInfo.GetDerEncoded());


            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(kp.Private);
            pemWriter.Writer.Flush();

            string privateKey = textWriter.ToString();

            byte[] privatekey_byte = Encoding.ASCII.GetBytes(privateKey);

            string pathPrivateKey = Path.Combine(pathDir, "privateKey.pem");
            using (FileStream outStream = new FileStream(pathPrivateKey, FileMode.Create, FileAccess.ReadWrite))
            {
                outStream.Write(privatekey_byte, 0, privatekey_byte.Length);
            }

            string pathDirectoryZip = Path.Combine(wwwPath, "Certificate_Key");

            ZipFile.CreateFromDirectory(pathDir, pathDirectoryZip, System.IO.Compression.CompressionLevel.Optimal, false);

            const string contentType = "application/zip";
            httpContext.Response.ContentType = contentType;
            var result = new FileContentResult(System.IO.File.ReadAllBytes(pathDirectoryZip), contentType);


            System.IO.DirectoryInfo dir = new DirectoryInfo(pathDir);
            foreach (FileInfo files in dir.GetFiles())
            {
                files.Delete();
            }
            Directory.Delete(pathDir);
            System.IO.File.Delete(pathDirectoryZip);

            result.FileDownloadName = certificateName + ".zip";


            return result;

        }
    }
}
