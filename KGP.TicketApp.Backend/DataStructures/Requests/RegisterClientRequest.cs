namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class RegisterClientRequest : EditRegisterUserRequest
    {
        public DateTime DateOFBirth { get; set; }
    }
}
