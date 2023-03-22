namespace KGP.TicketApp.Backend.Options
{
    public record ApplicationOptions
    {
        public string DatabaseConnectionString { get; set; } = null!;
    }
}
