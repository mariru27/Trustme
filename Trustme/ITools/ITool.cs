using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Trustme.ITools
{
    interface ITool
    {
        public string ComputeHash(string input, HashAlgorithm algorithm);

    }
}
