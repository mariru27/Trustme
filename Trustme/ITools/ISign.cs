using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto;
using Trustme.Tools.ToolsModels;

namespace Trustme.ITools
{
    public interface ISign
    {
        public SignModel SignDocumentTest(IFormFile pkfile, IFormFile docfile, int certificates, HttpContext httpContext);
        public string SignDocument(SignModel signModel);
        public ISigner VerifySignature(VerifySignatureModel verifySignatureModel);

    }
}
