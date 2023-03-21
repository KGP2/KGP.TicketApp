namespace KGP.TicketApp.Model.Requests
{
    public record EditRegisterUserRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
    }
}
