using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Post methods

        /// <summary>
        /// Log in as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("clients/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClientsLogin([FromBody] LoginCredentialsRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Log in as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>      
        [HttpPost("organizers/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organizer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizersLogin([FromBody] LoginCredentialsRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Register as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>     
        [HttpPost("registerOrganizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterOrganizer([FromBody] RegisterOrganizerRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Register as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("registerClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterClient([FromBody] RegisterClientRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Edit specified client.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>      
        [HttpPost("editClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditClient([FromBody] EditRegisterUserRequest request, string id)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Edit specified organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>      
        [HttpPost("editOrganizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditOrganizer([FromBody] EditRegisterUserRequest request, string id)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get all clients.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("clients")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClients([FromBody] TakeSkipRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get all organizers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("organizers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organizer[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizers([FromBody] TakeSkipRequest request)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Block a specified organizer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>     
        [HttpPost("blockOrganizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostBlockOrganizer(string id)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Block a specified client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>      
        [HttpPost("blockClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostBlockClient(string id)
        {
            return BadRequest();
        }

        #endregion

        #region Get methods

        /// <summary>
        /// [NYI] Get details of specified client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("clients/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(string id)
        {
            return BadRequest();
        }

        /// <summary>
        /// [NYI] Get details of specified organizer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("organizers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organizer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrganizer(string id)
        {
            return BadRequest();
        }

        #endregion
    }
}
