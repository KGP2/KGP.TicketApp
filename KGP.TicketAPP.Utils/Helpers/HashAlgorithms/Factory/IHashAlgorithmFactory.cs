using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory
{
    public interface IHashAlgorithmFactory
    {
        IHashAlgorithm Create(string algorithmName);
    }
}
