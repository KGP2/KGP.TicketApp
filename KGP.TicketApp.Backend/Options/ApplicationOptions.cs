using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;

namespace KGP.TicketApp.Backend.Options
{
    public record ApplicationOptions
    {
        public string DatabaseConnectionString { get; set; } = null!;
        public string BlobConnectionString { get; set; } = null!;
        public string JwtKey { get; set; } = null!;
        public string JwtIssuer { get; set; } = null!;
        public HashAlgorithmType HashAlgorithm { get; set; }
        public string TicketsCointainerName { get; set; } = null!;
    }
}
