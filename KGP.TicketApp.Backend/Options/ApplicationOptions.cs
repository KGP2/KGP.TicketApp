namespace KGP.TicketApp.Backend.Options
{
    public record ApplicationOptions
    {
        public string DatabaseConnectionString { get; set; } = null!;
        public string JwtKey { get; set; } = null!;
        public string JwtIssuer { get; set; } = null!;
    }
}
