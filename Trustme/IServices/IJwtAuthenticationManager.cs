using Trustme.Models;

namespace Trustme.IServices
{
    public interface IJwtAuthenticationManager
    {
        public string GenerateTokenForUser(User user);

    }
}
