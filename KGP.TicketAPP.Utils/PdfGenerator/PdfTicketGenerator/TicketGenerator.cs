using iText.Kernel.Geom;
using iText.Layout;
using KGP.TicketApp.Model.Database.Tables;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator.PdfTicketGenerator
{
    public class TicketGenerator : PdfGenerator<TicketGeneratorInitData>
    {
        #region Fields
        private const int QRCodeSize = 129;
        private const int QRCodeX = 100;
        private const int QRCodeY = 600;
        private const int defaultBreak = 25;
        #endregion

        #region Properties
        public Ticket Ticket { get; set; }
        #endregion

        #region Interface methods
        public void Generate()
        {
            base.Generate();

            document.AddQRCode(pdfDocument, $"{Ticket.Id}", new Rectangle(QRCodeX, QRCodeY, QRCodeSize, QRCodeSize));
            document.AddTextToPosition(QRCodeX + QRCodeSize + defaultBreak, QRCodeY + 4 * defaultBreak, "Posiadacz biletu:", 200, true);
            document.AddTextToPosition(QRCodeX + QRCodeSize + defaultBreak, QRCodeY + 3 * defaultBreak, $"{Ticket.Owner.Name} {Ticket.Owner.Surname}", 200);
            document.AddTextToPosition(QRCodeX + QRCodeSize + defaultBreak, QRCodeY + 2 * defaultBreak, "Wydarzenie:", 200, true);
            document.AddTextToPosition(QRCodeX + QRCodeSize + defaultBreak, QRCodeY + 1 * defaultBreak, $"{Ticket.Event.Name} - {Ticket.Event.Date.ToString("g")}", 200);
        }

        public void InitData(TicketGeneratorInitData initData)
        {
           base.InitData(initData);
        }

        public void Save(Action<byte[]> saveFun)
        {
            base.Save(saveFun);
        }

        #endregion
    }
}
