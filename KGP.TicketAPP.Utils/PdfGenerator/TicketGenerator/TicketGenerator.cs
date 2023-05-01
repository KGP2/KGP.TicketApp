using iText.Layout;
using KGP.TicketApp.Model.DTOs;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator.TicketGenerator
{
    public class TicketGenerator : PdfGenerator, IPdfGenerator<TicketGeneratorInitData>
    {
        #region Fields
        private ClientDTO client;
        #endregion

        #region Properties
        public ClientDTO Client { get; set; }
        #endregion

        #region Interface methods
        public void Generate()
        {
            throw new NotImplementedException();
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
