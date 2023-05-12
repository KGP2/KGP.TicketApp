using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = iText.Layout.Document;

namespace KGP.TicketApp.Utils.PdfGenerator
{
    public interface IPdfGenerator<T> where T : IPdfGeneratorInitData<T>
    {
        void InitData(T initData);
        void Generate();
        void Save(Action<byte[]> saveFun);
    }
}
