using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IPendingRepository
    {
        public IEnumerable<Pending> ListAllPendingRequests(User user);
        public void UserAcceptsPendingFromUsername(User user, string username);
    }
}
