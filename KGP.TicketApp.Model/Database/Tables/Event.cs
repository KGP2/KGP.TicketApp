using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Event
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public DateTime Date { get; set; }

        public Location Place { get; set; } = null!;

        public Organizer Organizer { get; set; } = null!;

        public int ParticipantsLimit { get; set; }

        public List<Client> ParticipantsList { get; set; } = null!;

        [MaxLength(200)]
        public string Price { get; set; } = null!;

        public DateTime TicketSaleStartDate { get; set; }

        public DateTime TicketSaleEndDate { get; set; }
    }
}
