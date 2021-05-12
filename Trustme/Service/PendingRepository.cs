using System.Collections.Generic;
using System.Linq;
using Trustme.Data;
using Trustme.Models



namespace Trustme.Service
{
    public class PendingRepository
    {
        private AppContext _context;
        public PendingRepository(AppContext appContext)
        {
            _context = appContext;
        }

        public IEnumerable<PendingRequest> ListAllPedingRequests(User user)
        {
            _context.User.Where(a => a.UserId == user.UserId).Join(_context.PendingRequest,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new PendingRequest { TimePendingRequest = p.TimePendingRequest, User = p.User, Username = p.Username })
                .ToList();
        }
    }
}
