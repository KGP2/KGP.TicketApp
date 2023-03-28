using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        #region Post methods

        /// <summary>
        /// [NYI] Create a ticket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostTickets([FromBody] PostTicketRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Validate specified ticket.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns> 
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

        /// <summary>
        /// [NYI] Get all tickets.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTickets()
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get details of specified ticket.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpGet("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicket(string ticketId)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get all tickets owned by specified owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet("/ticketsByOwner/{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ticket[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicketsByOwner(string ownerId)
        {
            return BadRequest();
        }

        #endregion
    }
}
