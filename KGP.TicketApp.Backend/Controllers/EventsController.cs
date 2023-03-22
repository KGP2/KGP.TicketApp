using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        #region Post methods

        // POST events    
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostEvents([FromBody] Event request)
        {
            return BadRequest();
        }

        // POST events/{id}      
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

        // GET events
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvents()
        {
            int x = 0;
            return BadRequest();
        }

        // GET events/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public IActionResult GetEvent(string id)
        {
            return BadRequest();
        }

        // GET eventsByOrganizer/{organizerId}
        [Route("/eventsByOrganizer/{organizerId}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsByOrganizer(string organizerId)
        {
            return BadRequest();
        }

        // GET eventList
        [Route("/eventList")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsList([FromBody] Guid[] ids)
        {
            return BadRequest();
        }

        #endregion

        #region Delete methods

        // DELETE events/{id}
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
