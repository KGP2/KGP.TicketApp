namespace KGP.TicketApp.Model.Requests
{
    public record LoginCredentialsRequest
    {
        #region Properties
        public string Email
        {
            get; set;
        }
        public string Password
        {
            get;
            set;
        }
        #endregion
    }
}
