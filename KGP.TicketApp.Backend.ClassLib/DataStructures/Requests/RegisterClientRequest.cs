namespace KGP.TicketApp.Backend.Libraries.DataStructures.Requests
{
    public record RegisterClientRequest : EditRegisterUserRequest
    {
        public DateTime DateOFBirth { get; set; }
    }
}
