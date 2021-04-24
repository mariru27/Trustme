using Trustme.Models;

namespace Trustme.IServices
{
    interface IJwtAuthenticationManager
    {
        public string GenerateTokenForUser(User user);

    }
}
