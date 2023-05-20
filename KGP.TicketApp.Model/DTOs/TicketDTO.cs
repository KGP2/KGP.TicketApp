using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Model.DTOs
{
    // TODO: model from database (generated with EF)
    public record TicketDTO
    {
        public string? TicketId { get; set; }
        public string? EventId { get; set; }

        public static TicketDTO FromDatabaseTicket(Ticket ticket)
        {
            return new TicketDTO()
            {
                EventId = ticket.Event.Id.ToString(),
                TicketId = ticket.Id.ToString(),
            };
        }
    }
}
