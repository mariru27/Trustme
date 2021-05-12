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

        public IEnumerable<Pending> ListAllPendingRequests(User user)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new Pending { TimePendingRequest = p.TimePendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending })
                .ToList();
        }

        public void UserAcceptsPendingFromUsername(User user, string username)
        {
            var pending = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimePendingRequest = p.TimePendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending })
            .ToList().Where(u => u.UsernameWhoSentPending == username).SingleOrDefault();

            if (pending != null)
            {
                //User accept pending from username
                pending.Accepted = true;

                //update and save changes
                //_context.Update(pending);
                //_context.SaveChanges();
            }
        }
    }
}
