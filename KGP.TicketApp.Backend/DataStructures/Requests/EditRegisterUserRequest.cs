﻿namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class EditRegisterUserRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
    }
}
