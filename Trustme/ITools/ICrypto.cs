using System.Security.Cryptography;

namespace Trustme.ITools
{
    public interface ICrypto
    {
        public string ComputeHash(string input, HashAlgorithm algorithm);
        public string RandomString(int length);

    }
}
