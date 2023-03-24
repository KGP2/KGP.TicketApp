using KGP.TicketApp.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace KGP.TicketApp.Model.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpUserTypeConversion(modelBuilder);
            SetUpUserDiscriminatorColumn(modelBuilder);
            SetUpEventOwnershipOfLocation(modelBuilder);
        }

        private static void SetUpUserTypeConversion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.UserType)
                .HasConversion<string>();
        }

        static void SetUpUserDiscriminatorColumn(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator(user => user.UserType)
                .HasValue(typeof(User), User.Types.User)
                .HasValue(typeof(Organizer), User.Types.Organizer)
                .HasValue(typeof(Client), User.Types.Client);
        }

        private static void SetUpEventOwnershipOfLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .OwnsOne(e => e.Place);
        }
    }
}
