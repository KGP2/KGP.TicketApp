using KGP.TicketApp.Utils.PdfGenerator;
using KGP.TicketAPP.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Backend.Tests.Utils.PdfGenerator
{
    public class TicketGeneratorTests : GeneratorTests
    {
        private PdfGeneratorService generatorService;

        [SetUp]
        public void Setup()
        {
            generatorService = new PdfGeneratorService();
        }

        #if DEBUG
        [Test]
        #endif
        // Do testowania lokalnie i w celach dokumentacyjnych
        public void TicketPdfGenerationTests()
        {
            generatorService.TicketGenerator.InitData(new TicketApp.Utils.PdfGenerator.PdfTicketGenerator.TicketGeneratorInitData()
            {
                ticket = new Model.Database.Tables.Ticket
                {
                    Event = new Model.Database.Tables.Event
                    {
                        Name = "test123",
                        Date = DateTime.Now
                    },
                    Id = Guid.NewGuid(),
                    Owner = new Model.Database.Tables.Client
                    {
                        Name = "testname",
                        Surname = "testsurname"
                    }
                }
            });

            generatorService.TicketGenerator.Generate();
            File.Exists(ticketName).Should().BeTrue();
            generatorService.TicketGenerator.Save(SaveLocally);
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}

