namespace KGP.TicketApp.Model.Requests
{
    public record RegisterClientRequest : EditRegisterUserRequest
    {
        public DateTime DateOFBirth { get; set; }
    }
}
