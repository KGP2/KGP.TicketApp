using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Organizer : User
    {
        public virtual List<Event> Events { get; set; } = null!;

        [MaxLength(200)]
        public string CompanyName { get; set; } = null!;
    }
}
