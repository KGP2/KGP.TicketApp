using KGP.TicketApp.Backend.Helpers;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("tickets")]
    [ApiController]
    [Authorize(AuthenticationSchemes = $"{JwtTokenHelper.Organizer},{JwtTokenHelper.Client}")]
    public class TicketsController : ControllerBase
    {
        #region Fields

        private IRepositoryWrapper repositoryWrapper;

        #endregion

        #region Constructors

        public TicketsController(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        #endregion

        #region Post methods

        /// <summary>
        /// Create a ticket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostTickets([FromBody] PostTicketRequest request)
        {
            var Event = repositoryWrapper.EventRepository.GetById(request.EventId);
            if (Event == null)
                return NotFound("Event does not exists.");
            
            var user = repositoryWrapper.ClientRepository.GetById(request.UserId);
            if (user == null) 
                return NotFound("User does not exists.");

            repositoryWrapper.TicketRepository.Create(new Ticket()
            {
                IsValidated = false,
                Owner = user,
                Event = Event
            });

            repositoryWrapper.Save();

            return Ok();
        }

        /// <summary>
        /// Validate specified ticket.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns> 
        [HttpPost("validate/{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostValidateTicket(Guid ticketId)
        {
            var ticket = repositoryWrapper.TicketRepository.GetById(ticketId);
            if (ticket == null)
                return NotFound("Ticket not found.");
            if (ticket.IsValidated == true)
                return BadRequest("Ticket was already validated.");

            ticket.IsValidated = true;
            repositoryWrapper.TicketRepository.Update(ticket);
            repositoryWrapper.Save();
            return Ok();
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Get tickets.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTickets(TakeSkipRequest request)
        {
            var tickets = repositoryWrapper.TicketRepository.TakeSkip(request.Take, request.Skip);
            if (tickets == null)
                return BadRequest("Returned ticket query was null. Contact administrator.");

            return Ok(tickets.Select(it => TicketDTO.FromDatabaseTicket(it)).ToArray());
        }

        /// <summary>
        /// Get details of specified ticket.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpGet("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicket(Guid ticketId)
        {
            var ticket = repositoryWrapper.TicketRepository.GetById(ticketId);
            if (ticket == null)
                return NotFound("Ticket with this id does not exists");

            return Ok(TicketDTO.FromDatabaseTicket(ticket));
        }

        /// <summary>
        /// Get all tickets owned by specified owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [HttpGet("/ticketsByOwner/{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTicketsByOwner(Guid ownerId)
        {
            var tickets = repositoryWrapper.TicketRepository.GetTicketsByOwner(ownerId);
            if (tickets == null || tickets.Count() == 0)
                return NotFound($"Cannot found tickets for requested owner");

            return Ok(tickets.Select(it => TicketDTO.FromDatabaseTicket(it)).ToArray());
        }

        #endregion
    }
}
