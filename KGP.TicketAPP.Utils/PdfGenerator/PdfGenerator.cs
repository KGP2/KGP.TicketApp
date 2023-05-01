using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator
{
    public abstract class PdfGenerator : IPdfGenerator<IPdfGeneratorInitData>, IDisposable
    {
        #region Fields
        private PdfWriter pdfWriter;
        private PdfDocument pdfDocument;
        private Document document;
        private MemoryStream stream;
        #endregion

        #region Interface methods
        public void Dispose()
        {
            document.Close();
            pdfDocument.Close();
            pdfWriter.Close();
            stream.Close();
        }

        public void Generate()
        {
            stream = new MemoryStream();
            pdfWriter = new PdfWriter(stream);
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);
        }

        public void InitData(IPdfGeneratorInitData initData)
        {
            initData.Init(this);
        }

        public void Save(Action<byte[]> saveFun)
        {
            saveFun(stream.ToArray());
        }

        #endregion
    }
}
