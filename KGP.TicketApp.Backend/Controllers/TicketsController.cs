using KGP.TicketApp.Backend.Libraries.DataStructures.DTOs;
using KGP.TicketApp.Backend.Libraries.DataStructures.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        #region Post methods

        // POST tickets    
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostTickets([FromBody] PostTicketRequest request)
        {
            return BadRequest();
        }

        // POST tickets/validate/{ticketId}   
        [HttpPost("validate/{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostValidateTicket(string ticketId)
        {
            return BadRequest();
        }

        #endregion

        #region Get methods

        // GET tickets
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTickets()
        {
            return BadRequest();
        }

        // GET tickets/{ticketId}
        [HttpGet("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicket(string ticketId)
        {
            return BadRequest();
        }

        // GET ticketsByOwner/ownerId
        [HttpGet("/ticketsByOwner/{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicketsByOwner(string ownerId)
        {
            return BadRequest();
        }

        #endregion
    }
}
