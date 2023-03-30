using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory
{
    public class HashAlgorithmFactory : IHashAlgorithmFactory
    {
        public IHashAlgorithm Create(string algorithmName)
        {
            switch (algorithmName)
            {
                case "BCrypt":
                    return new BCryptAlgorithm();
                default:
                    throw new ArgumentNullException(nameof(algorithmName), "Bad hash algorithm name, check config file");
            }
        }
    }
}
