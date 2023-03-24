using System.ComponentModel.DataAnnotations.Schema;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Ticket
    {
        public Guid Id { get; set; }

        public virtual Organizer Owner { get; set; } = null!;

        public virtual Event Event { get; set; } = null!;

        public bool IsValidated { get; set; }
    }
}
