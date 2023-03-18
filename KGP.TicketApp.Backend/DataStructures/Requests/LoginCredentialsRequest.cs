namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class LoginCredentialsRequest
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
