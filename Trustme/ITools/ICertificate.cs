using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Tools.ToolsModels;

namespace Trustme.ITools
{
    public interface ICertificate
    {
        public KeyPairCertificateGeneratorModel GenereateCertificate(int keySize);
    }
}
