using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Model.Database.Tables
{
    [Table("ClientEvent_Participatings")]
    public record ClientEvent_Participating
    {
        public Event ParticipatedEvent { get; set; } = null!;

        public Guid ParticipatedEventId { get; set; }

        public Client ParticipatingClient { get; set; } = null!;

        public Guid ParticipatingClientId { get; set; }
    }
}
