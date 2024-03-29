﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Text;
using Trustme.IServices;
using Trustme.ITools;
using Trustme.Models;
using Trustme.Tools.ToolsModels;

namespace Trustme.Tools
{
    public class Sign : ISign
    {
        [Obsolete]
        private readonly IHostingEnvironment Environment;
        private readonly IKeyRepository _KeyRepository;
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private string wwwfilePath;
        private readonly ICrypto _Crypto;

        [Obsolete]
        public Sign(IHostingEnvironment hostingEnvironment, ICrypto crypto, IKeyRepository keyRepository, IHttpRequestFunctions httpRequestFunctions)
        {
            Environment = hostingEnvironment;
            _KeyRepository = keyRepository;
            _HttpRequestFunctions = httpRequestFunctions;
            _Crypto = crypto;
        }

        [Obsolete]
        public SignModel SignDocumentTest(IFormFile pkfile, IFormFile docfile, int certificates, HttpContext httpContext)
        {

            SignModel signModel = new SignModel();
            signModel.validKey = true;
            wwwfilePath = this.Environment.WebRootPath; //we are using Temp file name just for the example. Add your own file path.c
            string dirName = "DirForPK_" + _Crypto.RandomString(6);
            wwwfilePath = Path.Combine(wwwfilePath, dirName);

            //create folder where to store key
            if (!Directory.Exists(wwwfilePath))
                Directory.CreateDirectory(wwwfilePath);
            var filePath = Path.Combine(wwwfilePath, pkfile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                pkfile.CopyTo(stream);
            }

            byte[] fileBytesdoc;
            using (var ms = new MemoryStream())
            {
                docfile.CopyTo(ms);
                fileBytesdoc = ms.ToArray();
            }
            //read private key and phrase
            string keypath = Path.Combine(wwwfilePath, pkfile.FileName);
            var reader = System.IO.File.OpenText(keypath);
            var keypem = new PemReader(reader);
            var o = keypem.ReadObject();
            if (o == null)
            {
                signModel.validKey = false;
                return signModel;
            }
            try
            {
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)o;
                AsymmetricKeyParameter privatekeyy = keyPair.Private;

                //just for test
                //-------begin--test------------------------------------------------------
                string testmessage = "this is a test message";
                byte[] testmessagetyte = Encoding.ASCII.GetBytes(testmessage);

                var currentKey = _KeyRepository.GetKey(_HttpRequestFunctions.GetUserId(httpContext), certificates);

                //phrase public key
                var readerPublickey = new StringReader(currentKey.PublicKey);
                var pemPublicKey = new PemReader(readerPublickey);

                var publickey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemPublicKey.ReadObject();

                reader.Close();

                ISigner signtest = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
                signtest.Init(true, privatekeyy);
                signtest.BlockUpdate(testmessagetyte, 0, testmessagetyte.Length);
                var signaturetest = signtest.GenerateSignature();
                string signatureteststring = Convert.ToBase64String(signaturetest);

                signtest.Init(false, publickey);
                signtest.BlockUpdate(testmessagetyte, 0, testmessagetyte.Length);

                byte[] signaturetestbyte = Convert.FromBase64String(signatureteststring);

                var verifytest = signtest.VerifySignature(signaturetestbyte);
                //------end--test-----------------------------------------------------------------

                signModel.fileBytesdoc = fileBytesdoc;
                signModel.keypath = keypath;
                signModel.privatekeyy = privatekeyy;
                signModel.reader = reader;
                signModel.verifytest = verifytest;


            }
            catch (Exception e)
            {

            }

            return signModel;
        }


        public string SignDocument(SignModel signModel)
        {
            ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
            sign.Init(true, signModel.privatekeyy);
            sign.BlockUpdate(signModel.fileBytesdoc, 0, signModel.fileBytesdoc.Length);
            var signature = sign.GenerateSignature();
            string signaturestring = Convert.ToBase64String(signature);

            signModel.reader.Close();
            System.IO.File.Delete(signModel.keypath);
            Directory.Delete(wwwfilePath);
            //TempData["signature"] = signaturestring;
            return signaturestring;

        }

        public ISigner VerifySignature(VerifySignatureModel verifySignatureModel)
        {
            //get public key by name from database, use key to decrypt

            Key userKey = _KeyRepository.GetKeyByCertificateName(verifySignatureModel.Username, verifySignatureModel.CertificateName);
            string publicKeystring = userKey.PublicKey;
            //string publicKeystring = admin.getPublicKeyByCertificateName(username, certificateName);


            var reader = new StringReader(publicKeystring);
            var keypem = new PemReader(reader);

            var publickey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)keypem.ReadObject();

            reader.Close();

            byte[] fileBytesdoc;

            using (var ms = new MemoryStream())
            {
                verifySignatureModel.Document.CopyTo(ms);
                fileBytesdoc = ms.ToArray();
            }

            ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
            sign.Init(false, publickey);
            sign.BlockUpdate(fileBytesdoc, 0, fileBytesdoc.Length);
            return sign;
        }
    }
}
