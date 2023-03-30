using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Helpers.HashAlgorithms
{
    public class BCryptAlgorithm : IHashAlgorithm
    {
        public string Hash(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public bool Verify(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
