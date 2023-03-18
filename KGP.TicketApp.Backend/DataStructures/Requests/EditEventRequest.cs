using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class EditEventRequest
    {
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public int ParticipiantsLimit { get; set; }
        public double Price { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndTime { get; set; }
    }
}
