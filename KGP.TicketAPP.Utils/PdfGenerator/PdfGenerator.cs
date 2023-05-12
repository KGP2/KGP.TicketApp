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
    public abstract class PdfGenerator<T> : IPdfGenerator<T>, IDisposable where T : IPdfGeneratorInitData<T>
    {
        #region Fields
        protected PdfWriter pdfWriter;
        protected PdfDocument pdfDocument;
        protected Document document;
        protected MemoryStream stream;
        #endregion

        #region Interface methods
        public void Dispose()
        {
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

        public void InitData(T initData)
        {
            initData.Init(this);
        }

        public void Save(Action<byte[]> saveFun)
        {
            document.Close();
            saveFun(stream.ToArray());
        }

        #endregion
    }
}
