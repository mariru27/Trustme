
using Microsoft.Extensions.Configuration;

namespace Trustme.Service
{
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration _Configuration;
        private readonly string privateKey;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _Configuration = configuration;
            privateKey = _Configuration.GetValue<string>("PrivateKey");
        }


    }
}
