using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KGP.TicketApp.Model.Database.Tables
{
    public record User
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string? Surname { get; set; }

        /// <remarks>
        /// Length set to 256 according to discussion:
        /// <see href="https://stackoverflow.com/questions/386294/what-is-the-maximum-length-of-a-valid-email-address"/>
        /// </remarks>
        [MaxLength(256)]
        public string Email { get; set; } = null!;

        /// <remarks>
        /// Property used as a discriminator column in Table-per-Hierarchy inheritance model, as described in:
        /// <see href="https://learn.microsoft.com/en-us/ef/core/modeling/inheritance"/>
        /// </remarks>        
        [Column(TypeName = "nvarchar(50)")]
        public Types UserType { get; set; }

        public enum Types
        {
            User,
            Organizer,
            Client
        }
    }
}