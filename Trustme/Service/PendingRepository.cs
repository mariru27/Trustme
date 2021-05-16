using System;
using System.Collections.Generic;
using System.Linq;
using Trustme.IServices;
using Trustme.Models;


namespace Trustme.Service
{
    public class PendingRepository : IPendingRepository
    {
        private Data.AppContext _context;
        public PendingRepository(Data.AppContext appContext)
        {
            _context = appContext;
        }

        public IEnumerable<Pending> ListAllPendingRequests(User user)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
                u => u.UserId,
                p => p.User.UserId,
                (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
                .ToList();
        }

        public void MarkUserAcceptPendingFromUsername(User user, string username)
        {
            var pending = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
            .ToList().Where(u => u.UsernameWhoSentPending == username).SingleOrDefault();

            if (pending != null)
            {
                //User accept pending from username
                pending.Accepted = true;
                pending.TimeAcceptedPendingRequest = DateTime.Now;

                //update and save changes
                //_context.Update(pending);
                //_context.SaveChanges();
            }
        }

        public bool CheckAcceptedPendingFromUsername(User user, string username)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
            .ToList().Where(u => u.UsernameWhoSentPending == username).Any();
        }

        public void AddPendingRequest(User user, string UsernameWhoSentPending)
        {
            var peding = new Pending
            {
                User = user,
                UsernameWhoSentPending = UsernameWhoSentPending
            };
            _context.Pendings.Add(peding);
            _context.SaveChanges();
        }

        public void Block(User user, int IdPedingUsers)
        {
            var pending = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
            .ToList().Where(u => u.IdPedingUsers == IdPedingUsers).SingleOrDefault();
            pending.Blocked = true;

            _context.Update(pending);
            _context.SaveChanges();
        }

        public Pending GetPending(User user, string username)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
           u => u.UserId,
           p => p.User.UserId,
           (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers)
           .ToList().Where(u => u.UsernameWhoSentPending == username).SingleOrDefault();
        }

        public Pending GetPending(User user, int IdPending)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
           u => u.UserId,
           p => p.User.UserId,
           (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers })
           .ToList().Where(u => u.IdPedingUsers == IdPending).SingleOrDefault();
        }
    }
}
