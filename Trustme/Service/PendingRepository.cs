using Trustme.Data;


namespace Trustme.Service
{
    public class PendingRepository
    {
        private AppContext _context;
        public PendingRepository(AppContext appContext)
        {
            _context = appContext;
        }
    }
}
