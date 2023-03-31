using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory
{
    public class HashAlgorithmFactory : IHashAlgorithmFactory
    {
        public IHashAlgorithm Create(HashAlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case HashAlgorithmType.BCrypt:
                    return new BCryptAlgorithm();
                default:
                    throw new ArgumentNullException(nameof(algorithmType), "Bad hash algorithm type, check config file");
            }
        }
    }
}
