using Microsoft.AspNetCore.Http;

namespace Trustme.ViewModels
{
    public class SignModel
    {
        public int IdUnsignedDocument { get; set; }
        public IFormFile PkFile { get; set; }
    }
}
