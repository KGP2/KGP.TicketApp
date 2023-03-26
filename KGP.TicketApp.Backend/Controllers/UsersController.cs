using BCrypt.Net;
using KGP.TicketApp.Backend.Helpers;
using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BCrypt;
using static KGP.TicketApp.Model.Database.Tables.User;
using KGP.TicketApp.Model.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        #region Fields

        private ApplicationOptions settings;
        private IRepositoryWrapper repositoryWrapper;

        #endregion

        #region Constructors
        public UsersController(IOptions<ApplicationOptions> settings, IRepositoryWrapper repositoryWrapper)
        {
            this.settings = settings.Value;
            this.repositoryWrapper = repositoryWrapper;
        }
        #endregion

        #region Post methods

        // POST users/clients/login
        [AllowAnonymous]
        [HttpPost("clients/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClientsLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.ClientRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            Response.Headers.Add("Token", JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings));

            return Ok(new ClientDTO() { Id = user.Id.ToString(), Name = user.Name, Surname = user.Surname });
        }

        // POST users/organizers/login
        [AllowAnonymous]
        [HttpPost("organizers/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizersLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.OrganizerRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            Response.Headers.Add("Token", JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings));

            return Ok(new OrganizerDTO() { Id = user.Id.ToString(), Name = user.Name, Surname = user.Surname, CompanyName = user.CompanyName });
        }

        // POST users/registerOrganizer
        [AllowAnonymous]
        [HttpPost("registerOrganizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterOrganizer([FromBody] RegisterOrganizerRequest request)
        {
            repositoryWrapper.OrganizerRepository.Create(new Organizer()
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                UserType = Types.Organizer,
                CompanyName = request.CompanyName
            });
            return Ok();
        }

        // POST users/registerClient
        [AllowAnonymous]
        [HttpPost("registerClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterClient([FromBody] RegisterClientRequest request)
        {
            repositoryWrapper.ClientRepository.Create(new Client()
            {
                Email = request.Email,
                DateOfBirth = request.DateOFBirth,
                Name = request.Name,
                Surname = request.Surname,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                UserType = Types.Client
            });
            repositoryWrapper.Save();

            return Ok();
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(string id)
        {
            return BadRequest();
        }

        // GET users/organizers/{id}
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
