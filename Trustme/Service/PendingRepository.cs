using System.Collections.Generic;
using System.Linq;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Models;


namespace Trustme.Service
{
    public class PendingRepository : IPendingRepository
    {
        private AppContext _context;
        public PendingRepository(AppContext appContext)
        {
            _context = appContext;
        }

        public IEnumerable<PendingRequest> ListAllPedingRequests(User user)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.PendingRequest,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new PendingRequest { TimePendingRequest = p.TimePendingRequest, User = p.User, Username = p.Username })
                .ToList();
        }
    }
}
