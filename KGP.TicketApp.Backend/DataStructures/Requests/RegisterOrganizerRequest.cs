namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class RegisterOrganizerRequest : EditRegisterUserRequest
    {
        public string CompanyName { get; set; }
    }
}
