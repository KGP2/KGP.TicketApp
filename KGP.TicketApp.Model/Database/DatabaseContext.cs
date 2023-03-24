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
            SetUpClientEventLikingRelationship(modelBuilder);
            SetUpClientEventParticipatingRelationship(modelBuilder);
            SetUpClientTicketRelationship(modelBuilder);
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
                .HasValue(typeof(Organizer), User.Types.Organizer)
                .HasValue(typeof(Client), User.Types.Client);
        }

        private static void SetUpEventOwnershipOfLocation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .OwnsOne(e => e.Place);
        }

        private static void SetUpClientEventLikingRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.LikedEvents)
                .WithMany(e => e.LikingClients)
                .UsingEntity<ClientEvent_Liking>(
                j => j
                    .HasOne(l => l.LikedEvent)
                    .WithMany(e => e.ClientEvent_Likings)
                    .HasForeignKey(l => l.LikedEventId)
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j
                    .HasOne(l => l.LikingClient)
                    .WithMany(c => c.ClientEvent_Likings)
                    .HasForeignKey(l => l.LikingClientId)
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j
                    .HasKey(l => new {l.LikingClientId, l.LikedEventId})
                );
        }

        private static void SetUpClientEventParticipatingRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.ParticipatedEvents)
                .WithMany(e => e.ParticipantsList)
                .UsingEntity<ClientEvent_Participating>(
                j => j
                    .HasOne(l => l.ParticipatedEvent)
                    .WithMany(e => e.ClientEvent_Participatings)
                    .HasForeignKey(l => l.ParticipatedEventId)
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j
                    .HasOne(l => l.ParticipatingClient)
                    .WithMany(c => c.ClientEvent_Participatings)
                    .HasForeignKey(l => l.ParticipatingClientId)
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j
                    .HasKey(l => new { l.ParticipatingClientId, l.ParticipatedEventId })
                );
        }

        private static void SetUpClientTicketRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Owner)
                .WithMany(c => c.Tickets)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
