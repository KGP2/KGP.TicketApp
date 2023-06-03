using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Model.DTOs
{
    public record EventDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Place { get; set; }
        public string? OrganizerId { get; set; }
        public int? ParticipantsLimit { get; set; }
        public double? Price { get; set; }
        public DateTime? SaleStartDate { get; set; }
        public DateTime? SaleEndDate { get; set; }
        public string? Photo { get; set; }

        private static string FormatLocationString(Location location)
        {
            return $"{location.City}, {location.StreetName} ";
        }

        public static EventDTO FromDatabaseEvent(Event @event)
        {
            return new EventDTO
            {
                Id = @event.Id.ToString(),
                Name = @event.Name,
                Date = @event.Date,
                Place = FormatLocationString(@event.Place),
                OrganizerId = @event.Organizer.Id.ToString(),
                ParticipantsLimit = @event.ParticipantsLimit,
                Price = double.Parse(@event.Price), //TODO
                SaleStartDate = @event.TicketSaleStartDate,
                SaleEndDate = @event.TicketSaleEndDate,
                Photo = null //TODO
            };
        }
    }
}
