using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Model.DTOs
{
    public record ClientDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public static ClientDTO FromDatabaseUser(User user)
        {
            return new ClientDTO()
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
