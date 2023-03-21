namespace KGP.TicketApp.Backend.Libraries.DataStructures.Requests
{
    public record RegisterOrganizerRequest : EditRegisterUserRequest
    {
        public string CompanyName { get; set; }
    }
}
