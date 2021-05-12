using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IPendingRepository
    {
        public IEnumerable<PendingRequest> ListAllPedingRequests(User user);
    }
}
