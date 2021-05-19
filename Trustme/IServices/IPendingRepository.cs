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
        public void Block(User user, int IdPedingUsers);
        public Pending GetPending(User user, string username);
        public Pending GetPending(User user, int IdPending);
        public bool CheckBockedPendingFromUsername(User user, string username);

    }
}
