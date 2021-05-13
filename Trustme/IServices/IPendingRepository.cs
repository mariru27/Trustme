using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IPendingRepository
    {
        public IEnumerable<Pending> ListAllPendingRequests(User user);
        public void MarkUserAcceptPendingFromUsername(User user, string username);
        public bool CheckAcceptedPendingFromUsername(User user, string username);
        public void AddPendingRequest(User user, string UsernameWhoSentPending);

    }
}
