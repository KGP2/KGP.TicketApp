using System.ComponentModel.DataAnnotations.Schema;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Client : User
    {
        public List<Ticket> Tickets { get; set; } = null!;

        public List<Event> LikedEvents { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
    }
}
