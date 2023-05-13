using KGP.TicketApp.Backend.Helpers;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using KGP.TicketAPP.Utils.Extensions;
using KGP.TicketApp.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private const string EventNotFound = "Event not found.";

        private IRepositoryWrapper repositoryWrapper;
        private IEventRepository eventRepository => repositoryWrapper.EventRepository;

        public EventsController(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        #region Post methods

        /// <summary>
        /// Create an event.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtTokenHelper.Organizer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostEvents([FromBody] CreateEventRequest request)
        {
            eventRepository.Create(new Event
            {
                Name = request.Name,
                Date = request.Date,
                Place = new Location
                {
                    City = "TODO",
                    BuildingName = "TODO",
                    PostalCode = "TODO",
                    StreetName = "TODO",
                    StreetNumber = "TODO"
                }, // TODO: Fix when documentation updates
                Organizer = new Organizer { Id = this.GetCallingUserId() },
                Price = request.Price.ToString(), // TODO: Fix when documentation updates
                TicketSaleStartDate = request.SaleStartDate,
                TicketSaleEndDate = request.SaleStartDate,
                // TODO: photo
            });
            repositoryWrapper.Save();
            return Ok();
        }

        /// <summary>
        /// Edit specified event.
        /// </summary>
        /// <returns></returns>  
        [HttpPost("{id}")]
        [Authorize(AuthenticationSchemes = JwtTokenHelper.Organizer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditEvent([FromBody] EditEventRequest request, [FromRoute] Guid id)
        {
            var eventToEdit = eventRepository.GetById(id);

            if (eventToEdit == null)
            {
                return NotFound(EventNotFound);
            }

            if (eventToEdit.Organizer.Id != this.GetCallingUserId())
            {
                return Unauthorized();
            }

            EditEvent(eventToEdit, request);

            eventRepository.Update(eventToEdit);
            repositoryWrapper.Save();

            return Ok();
        }

        private static void EditEvent(Event eventToEdit, EditEventRequest request)
        {
            if (request.ParticipiantsLimit != null)
            {
                eventToEdit.ParticipantsLimit = request.ParticipiantsLimit.Value;
            }
            if (request.Date != null)
            {
                eventToEdit.Date = request.Date.Value;
            }
            if (request.SaleStartDate != null)
            {
                eventToEdit.TicketSaleStartDate = request.SaleStartDate.Value;
            }
            if (request.SaleEndTime != null)
            {
                eventToEdit.TicketSaleEndDate = request.SaleEndTime.Value;
            }
            if (request.Place != null)
            {
                eventToEdit.Place = new Location(); // TODO: Fix when documentation updates
            }
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [AllowAnonymous()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvents([FromQuery] GetEventsRequest request)
        {
            return Ok(eventRepository
                .GetByFilterFromRequest(request)
                .Select(e => EventDTO.FromDatabaseEvent(e)));
        }

        /// <summary>
        /// Get details of specified event.
        /// </summary>
        /// <returns></returns> 
        [HttpGet("{id}")]
        [AllowAnonymous()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEvent(Guid id)
        {
            return eventRepository.GetById(id) switch
            {
                null => NotFound(EventNotFound),
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
        [AllowAnonymous()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsByOrganizer(Guid organizerId)
        {
            return Ok(eventRepository
                .GetByOrganizerId(organizerId)
                .Select(e => EventDTO.FromDatabaseEvent(e)));
        }

        /// <summary>
        /// Get details of several specified events.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("/eventList")]
        [HttpGet()]
        [AllowAnonymous()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEventsList([FromBody] Guid[] ids)
        {
            return Ok(eventRepository
                .GetByIdList(ids)
                .Select(e => EventDTO.FromDatabaseEvent(e)));
        }

        #endregion

        #region Delete methods

        /// <summary>
        /// Delete specified event.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtTokenHelper.Organizer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvent([FromRoute] Guid id)
        {
            var @event = eventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound(EventNotFound);
            }

            if (@event.Organizer.Id != this.GetCallingUserId())
            {
                return Unauthorized();
            }

            eventRepository.Delete(@event);
            repositoryWrapper.Save();

            return Ok();
        }

        #endregion
    }
}
