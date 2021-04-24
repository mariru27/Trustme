
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Trustme.Models;

namespace Trustme.Service
{
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration _Configuration;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public string Authenticate(User user)
        {

            var tockenHandler = new JwtSecurityTokenHandler();

            var privateKey = _Configuration.GetValue<string>("PrivateKey");
            var

        }

    }
}
