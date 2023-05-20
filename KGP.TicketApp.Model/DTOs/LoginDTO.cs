using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Model.DTOs
{
    public record LoginDTO<TUser> where TUser : class
    {
        public string Token { get; set; } = null!;
        public DateTime? ExpiresAt { get; set; }
        public TUser User { get; set; } = null!;
    }
}
