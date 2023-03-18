using KGP.TicketApp.Backend.DataStructures.Requests;
using KGP.TicketApp.Backend.DataStructures.DTOs;
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

        // POST users/clients/login
        [HttpPost("clients/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClientsLogin([FromBody] LoginCredentialsRequest request)
        {
            return BadRequest();
        }

        // POST users/organizers/login        
        [HttpPost("organizers/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizersLogin([FromBody] LoginCredentialsRequest request)
        {
            return BadRequest();
        }

        // POST users/registerOrganizer       
        [HttpPost("registerOrganizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterOrganizer([FromBody] RegisterOrganizerRequest request)
        {
            return BadRequest();
        }

        // POST users/registerClient
        [HttpPost("registerClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterClient([FromBody] RegisterClientRequest request)
        {
            return BadRequest();
        }

        // POST users/editClient/{id}       
        [HttpPost("editClient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditClient([FromBody] EditRegisterUserRequest request, string id)
        {
            return BadRequest();
        }

        // POST users/editOrganizer/{id}       
        [HttpPost("editOrganizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditOrganizer([FromBody] EditRegisterUserRequest request, string id)
        {
            return BadRequest();
        }

        // POST users/clients
        [HttpPost("clients")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClients([FromBody] TakeSkipRequest request)
        {
            return BadRequest();
        }

        // POST users/organizers
        [HttpPost("organizers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizers([FromBody] TakeSkipRequest request)
        {
            return BadRequest();
        }

        // POST users/blockOrganizer/{id}       
        [HttpPost("blockOrganizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostBlockOrganizer(string id)
        {
            return BadRequest();
        }

        // POST users/blockClient/{id}       
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

        // GET users/clients/{id}
        [HttpGet("clients/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(string id)
        {
            return BadRequest();
        }

        // GET users/organizers/{id}
        [HttpGet("organizers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrganizer(string id)
        {
            return BadRequest();
        }

        #endregion

    }
}
