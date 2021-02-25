using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Tools.ToolsModels;
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
    }
}
