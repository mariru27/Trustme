using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.ITools
{
    public interface ICertificate
    {
        public void GenereateCertificate(int keySize);
    }
}
