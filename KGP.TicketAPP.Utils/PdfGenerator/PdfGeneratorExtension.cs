using iText.Barcodes;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator
{
    public static class PdfGeneratorExtension
    {
        #region Public methods
        public static void AddQRCode(this Document document, PdfDocument pdfDocument, string payload, Rectangle rect)
        {
            BarcodeQRCode barcodeQRCode = new BarcodeQRCode(payload);
            PdfFormXObject pdfFormXObject = barcodeQRCode.CreateFormXObject(ColorConstants.BLACK, pdfDocument);
            Image qrCodeImage = new Image(pdfFormXObject).SetWidth(rect.GetWidth()).SetHeight(rect.GetHeight());

            document.Add(new Paragraph().SetFixedPosition(rect.GetX(), rect.GetY(), rect.GetWidth()).Add(qrCodeImage));
        }

        public static void AddTextToPosition(this Document document, int x, int y, string text, int width = 200, bool bold = false)
        {
            var Text = new Text(text);

            if (bold)
                Text.SetBold();
            Paragraph p = new Paragraph(Text);
            p.SetFixedPosition(x, y, width);
            document.Add(p);
        }

        #endregion
    }
}
