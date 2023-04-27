using System.ComponentModel.DataAnnotations;

namespace KGP.TicketApp.Model.Requests
{
    public record TakeSkipRequest
    {
        [Range(0, int.MaxValue)]
        public int Take { get; set; }

        [Range(0, int.MaxValue)]
        public int Skip { get; set; }
    }
}
