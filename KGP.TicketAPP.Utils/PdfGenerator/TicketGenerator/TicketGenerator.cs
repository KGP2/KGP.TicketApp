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
    public class TicketGenerator : IPdfGenerator<TicketGeneratorInitData>
    {
        #region Fields
        private ClientDTO client;
        #endregion

        #region Interface methods
        public void Generate()
        {
            throw new NotImplementedException();
        }

        public void InitData(TicketGeneratorInitData initData)
        {
           this.client = initData.client;
        }

        public void Save(Action<Document> saveFun)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
