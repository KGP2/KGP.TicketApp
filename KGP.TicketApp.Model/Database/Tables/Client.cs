using System.ComponentModel.DataAnnotations.Schema;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Client : User
    {
        public List<Ticket> Tickets { get; set; } = null!;

        public ICollection<Event> LikedEvents { get; set; } = null!;

        public List<ClientEvent_Liking> ClientEvent_Likings { get; set; } = null!;

        public ICollection<Event> ParticipatedEvents { get; set; } = null!;

        public List<ClientEvent_Participating> ClientEvent_Participatings { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
    }
}
