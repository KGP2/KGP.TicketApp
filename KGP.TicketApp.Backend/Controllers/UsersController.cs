using KGP.TicketApp.Backend.Helpers;
using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static KGP.TicketApp.Model.Database.Tables.User;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory;
using KGP.TicketAPP.Utils.Validation;
using KGP.TicketApp.Backend.Validation;

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
        private IHashAlgorithm hashAlgorithm;
        private IValidationService validationService;

        #endregion

        #region Constructors
        public UsersController(IOptions<ApplicationOptions> settings, IRepositoryWrapper repositoryWrapper, IHashAlgorithmFactory hashAlgorithmFactory, IValidationService validationService)
        {
            this.settings = settings.Value;
            this.repositoryWrapper = repositoryWrapper;
            this.validationService = validationService;
            hashAlgorithm = hashAlgorithmFactory.Create(this.settings.HashAlgorithm);
        }
        #endregion

        #region Post methods

        // POST users/clients/login
        [AllowAnonymous]
        /// <summary>
        /// Log in as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("clients/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClientsLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.ClientRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!hashAlgorithm.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            Response.Headers.Add("Token", JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings.JwtKey, settings.JwtIssuer));

            return Ok(ClientDTO.FromDatabaseUser(user));
        }

        // POST users/organizers/login
        [AllowAnonymous]
        /// <summary>
        /// Log in as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>      
        [HttpPost("organizers/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizersLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.OrganizerRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!hashAlgorithm.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            Response.Headers.Add("Token", JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings.JwtKey, settings.JwtIssuer));

            return Ok(OrganizerDTO.FromDatabaseUser(user));
        }

        // POST users/registerOrganizer
        [AllowAnonymous]
        /// <summary>
        /// Register as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>     
        [HttpPost("registerOrganizer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterOrganizer([FromBody] RegisterOrganizerRequest request)
        {
            if (!RegisterValidation.ValidateRegister(request.Email, request.Password, validationService, out string error))
                return BadRequest(error);

            repositoryWrapper.OrganizerRepository.Create(new Organizer()
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Password = hashAlgorithm.Hash(request.Password),
                UserType = Types.Organizer,
                CompanyName = request.CompanyName
            });
            repositoryWrapper.Save();

            return Ok();
        }

        // POST users/registerClient
        [AllowAnonymous]
        /// <summary>
        /// Register as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("registerClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostRegisterClient([FromBody] RegisterClientRequest request)
        {
            if (!RegisterValidation.ValidateRegister(request.Email, request.Password, validationService, out string error))
                return BadRequest(error);

            repositoryWrapper.ClientRepository.Create(new Client()
            {
                Email = request.Email,
                DateOfBirth = request.DateOFBirth,
                Name = request.Name,
                Surname = request.Surname,
                Password = hashAlgorithm.Hash(request.Password),
                UserType = Types.Client
            });
            repositoryWrapper.Save();

            return Ok();
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO[]))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO[]))]
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
