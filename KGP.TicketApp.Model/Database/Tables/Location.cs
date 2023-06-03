using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record Location
    {
        [MaxLength(200)]
        public string City { get; set; } = null!;

        [MaxLength(200)]
        public string StreetName { get; set; } = null!;
    }
}
