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

        [MaxLength(200)]
        public string StreetNumber { get; set; } = null!;

        [MaxLength(200)]
        public string PostalCode { get; set; } = null!;

        [MaxLength(200)]
        public string BuildingName { get; set; } = null!;
    }
}
