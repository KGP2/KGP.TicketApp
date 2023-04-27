using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KGP.TicketApp.Model.Database.Tables
{
    [Index(nameof(Email), IsUnique = true)]
    public abstract record User
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
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Hashed password
        /// </summary>
        [MaxLength(1000)]
        public string Password { get; set; } = null!;

        /// <remarks>
        /// Property used as a discriminator column in Table-per-Concrete inheritance model, as described in:
        /// <see href="https://learn.microsoft.com/en-us/ef/core/modeling/inheritance"/>
        /// </remarks>        
        [Column(TypeName = "nvarchar(50)")]
        public Types UserType { get; set; }

        public enum Types
        {
            Organizer,
            Client
        }
    }
}