using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        #region Post methods

        /// <summary>
        /// [NYI] Create an event.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostEvents([FromBody] EventDTO request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Edit specified event.
        /// </summary>
        /// <param name="request"></param> 
        /// <returns></returns>  
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditEvent([FromBody] EditEventRequest request, string id)
        {
            return BadRequest();
        }

        #endregion

        #region Get methods

        /// <summary>
        /// [NYI] Get all events.
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvents()
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get details of specified event.
        /// </summary>
        /// <returns></returns> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEvent(string id)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get all events owned by specified organizer.
        /// </summary>
        /// <param name="organizerId"></param>
        /// <returns></returns> 
        [Route("/eventsByOrganizer/{organizerId}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsByOrganizer(string organizerId)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get details of several specified events.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("/eventList")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsList([FromBody] Guid[] ids)
        {
            return BadRequest();
        }

        #endregion

        #region Delete methods

        /// <summary>
        /// [NYI] Delete specified event.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvent(string id)
        {
            return BadRequest();
        }

        #endregion
    }
}
