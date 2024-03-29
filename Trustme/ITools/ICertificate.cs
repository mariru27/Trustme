﻿using Trustme.Tools.ToolsModels;
using Trustme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Trustme.ITools
{
    public interface ICertificate
    {
        public KeyPairCertificateGeneratorModel GenereateCertificate(int keySize);
        public void CrateAndStoreKeyUserInDB(User currentUser, KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel, Key key);

        public FileContentResult CreateCertificateFileAndPrivateKeyFile(KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel, string certificateName, HttpContext httpContext);
        public FileContentResult DoAllGenereateSaveInDBCreateCertificateAndPKFile(User currentUser, Key key, HttpContext httpContext);
        public FileContentResult GenerateCertificatePivateKey(string certificateName, string description, int keySize, HttpContext httpContext);


    }
}
