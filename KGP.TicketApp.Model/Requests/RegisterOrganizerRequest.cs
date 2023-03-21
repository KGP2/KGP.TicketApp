namespace KGP.TicketApp.Model.Requests
{
    public record RegisterOrganizerRequest : EditRegisterUserRequest
    {
        public string CompanyName { get; set; }
    }
}
