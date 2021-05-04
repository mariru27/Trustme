using Microsoft.AspNetCore.Http;

namespace Trustme.Service
{
    public class StaticSessionWrapper
    {
        public int DeliveredNumberUnsignedDocuments;
        private static HttpSessionState Session => HttpContext.Session;

        public StaticSessionWrapper()
        {

            DeliveredNumberUnsignedDocuments = 0;
        }
    }
}
