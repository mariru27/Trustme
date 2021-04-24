using AutoMapper.Configuration;

namespace Trustme.Service
{
    public class JwtAuthenticationManager
    {
        IConfiguration _Configuration;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
    }
}
