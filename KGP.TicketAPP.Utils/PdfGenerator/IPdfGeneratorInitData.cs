using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator
{
    public interface IPdfGeneratorInitData<T> where T : IPdfGeneratorInitData<T>
    {
        void Init(IPdfGenerator<T> generator);
    }
}
