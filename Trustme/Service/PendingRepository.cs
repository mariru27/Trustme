using Microsoft.EntityFrameworkCore;
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
                (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, Seen = p.Seen, UserId = p.User.UserId })
                .ToList().Where(a => a.Accepted == false && a.Blocked == false).ToList();
        }

        public int CountUnseenPendings(User user)
        {
            return ListAllPendingRequests(user).Where(a => a.Seen == false).Count();
        }

        public void MarkSeen(User user)
        {
            IEnumerable<Pending> pendings = ListAllPendingRequests(user);
            foreach (var p in pendings)
            {
                p.Seen = true;
                _context.Pendings.Update(p);
                _context.SaveChanges();
            }
        }

        public void MarkUserAcceptPendingFromUsername(User user, string username)
        {
            var pending = _context.User.AsNoTracking().Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
            .ToList().Where(u => u.UsernameWhoSentPending == username).SingleOrDefault();


            var local = _context.Set<Pending>()
            .Local
            .FirstOrDefault(entry => entry.IdPedingUsers.Equals(pending.IdPedingUsers));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            _context.Entry(pending).State = EntityState.Modified;

            // save 
            _context.SaveChanges();

            if (pending != null)
            {
                //User accept pending from username
                pending.Accepted = true;
                pending.TimeAcceptedPendingRequest = DateTime.Now;

                //update and save changes
                _context.Update(pending);
                _context.SaveChanges();
            }
        }
        public bool CheckBockedPendingFromUsername(User user, string username)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
            .ToList().Where(u => u.UsernameWhoSentPending == username && u.Blocked == true).Any();
        }

        public bool CheckAcceptedPendingFromUsername(User user, string username)
        {
            var result = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
            .ToList();
            return result.Any();
        }

        public void AddPendingRequest(User user, string UsernameWhoSentPending)
        {
            //verify if user is already added
            bool userAdded = _context.User.AsNoTracking().Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
            .ToList().Where(u => u.UsernameWhoSentPending == UsernameWhoSentPending).Any();

            if (userAdded == false)
            {
                var peding = new Pending
                {
                    User = user,
                    UsernameWhoSentPending = UsernameWhoSentPending
                };
                _context.Pendings.Add(peding);
                _context.SaveChanges();
            }

        }

        public void Block(User user, int IdPedingUsers)
        {
            var pending = _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
            u => u.UserId,
            p => p.User.UserId,
            (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
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
           (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
           .ToList().Where(u => u.UsernameWhoSentPending == username).SingleOrDefault();
        }

        public Pending GetPending(User user, int IdPending)
        {
            return _context.User.Where(a => a.UserId == user.UserId).Join(_context.Pendings,
           u => u.UserId,
           p => p.User.UserId,
           (u, p) => new Pending { TimeSentPendingRequest = p.TimeSentPendingRequest, User = p.User, UsernameWhoSentPending = p.UsernameWhoSentPending, IdPedingUsers = p.IdPedingUsers, TimeAcceptedPendingRequest = p.TimeAcceptedPendingRequest, Accepted = p.Accepted, Blocked = p.Blocked, UserId = p.User.UserId })
           .ToList().Where(u => u.IdPedingUsers == IdPending).SingleOrDefault();
        }
    }
}
