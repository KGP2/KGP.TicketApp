using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private IEventRepository eventRepository;

        public EventsController(IRepositoryWrapper repositoryWrapper)
        {
            this.eventRepository = repositoryWrapper.EventRepository;
        }

        #region Post methods

        /// <summary>
        /// Create an event.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostEvents([FromBody] EventDTO request)
        {
            // TODO
            // authorize 
            return BadRequest();
        }

        /// <summary>
        /// Edit specified event.
        /// </summary>
        /// <param name="request"></param> 
        /// <param name="id"></param> 
        /// <returns></returns>  
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditEvent([FromBody] EditEventRequest request, [FromQuery] string id)
        {
            // TODO
            // autorize
            return BadRequest();
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvents()
        {
            // TODO
            return BadRequest();
        }

        /// <summary>
        /// Get details of specified event.
        /// </summary>
        /// <returns></returns> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEvent(string id)
        {
            if (!Guid.TryParse(id, out var guid))
            {
                return BadRequest("Invalid GUID.");
            }

            return eventRepository.GetById(guid) switch
            {
                null => NotFound("Event not found."),
                Event @event => Ok(EventDTO.FromDatabaseEvent(@event))
            };
        }

        /// <summary>
        /// Get all events owned by specified organizer.
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns> 
        [Route("/eventsByOrganizer/{organizerId}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsByOrganizer(string organizerId)
        {
            if (!Guid.TryParse(organizerId, out var guid))
            {
                return BadRequest("Invalid GUID.");
            }

            return Ok(eventRepository.GetByOrganizerId(guid));
        }

        /// <summary>
        /// Get details of several specified events.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("/eventList")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsList([FromBody] Guid[] ids)
        {
            return Ok(eventRepository.GetByIdList(ids));
        }

        #endregion

        #region Delete methods

        /// <summary>
        /// Delete specified event.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvent(string id)
        {
            // TODO
            // authorize
            return BadRequest();
        }

        #endregion
    }
}
