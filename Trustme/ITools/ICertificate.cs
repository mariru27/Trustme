using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Tools.ToolsModels;
using Trustme.Models;

namespace Trustme.ITools
{
    public interface ICertificate
    {
        public KeyPairCertificateGeneratorModel GenereateCertificate(int keySize);
        public void CrateAndStoreKeyUserInDB(User currentUser, KeyPairCertificateGeneratorModel keyPairCertificateGeneratorModel, Key key);
    }
}
