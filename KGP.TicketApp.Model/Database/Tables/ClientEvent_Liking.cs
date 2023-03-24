using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Model.Database.Tables
{
    [Table("ClientEvent_Likings")]
    public record ClientEvent_Liking
    {
        public Event LikedEvent { get; set; } = null!;

        public Guid LikedEventId { get; set; }

        public Client LikingClient { get; set; } = null!;

        public Guid LikingClientId { get; set; }

    }
}
