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
using KGP.TicketApp.Repositories;
using KGP.TicketAPP.Utils.Extensions;
using Org.BouncyCastle.Crypto.Tls;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KGP.TicketApp.Backend.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = $"{JwtTokenHelper.Organizer},{JwtTokenHelper.Client}")]
    public class UsersController : ControllerBase
    {
        #region Fields

        private ApplicationOptions settings;
        private IRepositoryWrapper repositoryWrapper;
        private IHashAlgorithm hashAlgorithm;

        #endregion

        #region Constructors
        public UsersController(IOptions<ApplicationOptions> settings, IRepositoryWrapper repositoryWrapper, IHashAlgorithmFactory hashAlgorithmFactory)
        {
            this.settings = settings.Value;
            this.repositoryWrapper = repositoryWrapper;
            hashAlgorithm = hashAlgorithmFactory.Create(this.settings.HashAlgorithm);
        }
        #endregion

        #region Post methods

        // POST users/clients/login
        /// <summary>
        /// Log in as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("clients/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO<ClientDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClientsLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.ClientRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!hashAlgorithm.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            var token = JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings.JwtKey, settings.JwtIssuer, Types.Client, Request.Host.Host);
            Response.Cookies.Append("Token", token.token, token.options);

            return Ok(new LoginDTO<ClientDTO>
            {
                Token = token.token,
                ExpiresAt = token.options.Expires?.DateTime,
                User = ClientDTO.FromDatabaseUser(user)
            });
        }

        // POST users/organizers/login
        /// <summary>
        /// Log in as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>      
        [AllowAnonymous]
        [HttpPost("organizers/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO<OrganizerDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizersLogin([FromBody] LoginCredentialsRequest request)
        {
            var user = repositoryWrapper.OrganizerRepository.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Email doesn't exist in the system");
            if (!hashAlgorithm.Verify(request.Password, user.Password))
                return BadRequest("Password is incorrect");

            var token = JwtTokenHelper.CreateToken(request.Email, user.Id.ToString(), settings.JwtKey, settings.JwtIssuer, Types.Organizer, Request.Host.Host);
            Response.Cookies.Append("Token", token.token, token.options);

            return Ok(new LoginDTO<OrganizerDTO>
            {
                Token = token.token,
                ExpiresAt = token.options.Expires?.DateTime,
                User = OrganizerDTO.FromDatabaseUser(user)
            });
        }

        // POST users/registerOrganizer
        /// <summary>
        /// Register as an organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>     
        [AllowAnonymous]
        [TypeFilter(typeof(RegisterEditUserValidation), Arguments = new object[] { Types.Organizer, true })]
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
                Password = hashAlgorithm.Hash(request.Password),
                UserType = Types.Organizer,
                CompanyName = request.CompanyName
            });
            repositoryWrapper.Save();

            return Ok();
        }

        // POST users/registerClient
        /// <summary>
        /// Register as a client.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [TypeFilter(typeof(RegisterEditUserValidation), Arguments = new object[] { Types.Client, true })]
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
                Password = hashAlgorithm.Hash(request.Password),
                UserType = Types.Client
            });
            repositoryWrapper.Save();

            return Ok();
        }

        /// <summary>
        /// Edit specified client.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [TypeFilter(typeof(RegisterEditUserValidation), Arguments = new object[] { Types.Client, false })]
        [ServiceFilter(typeof(TokenValidation))]
        [HttpPost("editClient/{id}")]
        [Authorize(AuthenticationSchemes = JwtTokenHelper.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditClient([FromBody] EditRegisterUserRequest request, Guid id)
        {
            var clientToEdit = repositoryWrapper.ClientRepository.GetById(id);

            if (clientToEdit == null)
                return NotFound("Client not found");

            clientToEdit.UpdateUser(request);
            repositoryWrapper.ClientRepository.Update(clientToEdit);
            repositoryWrapper.Save();

            return Ok();
        }

        /// <summary>
        /// Edit specified organizer.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [TypeFilter(typeof(RegisterEditUserValidation), Arguments = new object[] { Types.Organizer, false })]
        [ServiceFilter(typeof(TokenValidation))]
        [HttpPost("editOrganizer/{id}")]
        [Authorize(AuthenticationSchemes = JwtTokenHelper.Organizer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostEditOrganizer([FromBody] EditRegisterUserRequest request, Guid id)
        {
            var organizerToEdit = repositoryWrapper.OrganizerRepository.GetById(id);

            if (organizerToEdit == null)
                return NotFound("Organizer not found");
            organizerToEdit.UpdateUser(request);
            repositoryWrapper.OrganizerRepository.Update(organizerToEdit);
            repositoryWrapper.Save();

            return Ok();
        }

        /// <summary>
        /// Get all clients.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(TakeSkipValidation))]
        [HttpPost("clients")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostClients([FromBody] TakeSkipRequest request)
        {
            return Ok(repositoryWrapper.ClientRepository
                .TakeSkip(request.Take, request.Skip)
                .Select(client => ClientDTO.FromDatabaseUser(client))
                .ToArray());
        }

        /// <summary>
        /// Get all organizers.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(TakeSkipValidation))]
        [HttpPost("organizers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostOrganizers([FromBody] TakeSkipRequest request)
        {
            return Ok(repositoryWrapper.OrganizerRepository
                .TakeSkip(request.Take, request.Skip)
                .Select(organizer => OrganizerDTO.FromDatabaseUser(organizer))
                .ToArray());
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
        public IActionResult PostBlockOrganizer(Guid id)
        {
            // TODO: Jak zrobimy admina
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
        public IActionResult PostBlockClient(Guid id)
        {
            // TODO: Jak zrobimy admina
            return BadRequest();
        }

        #endregion

        #region Get methods

        /// <summary>
        /// Get details of specified client.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("clients/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(Guid id)
        {
            return repositoryWrapper.ClientRepository.GetById(id) switch
            {
                null => NotFound("Client not found"),
                Client client => Ok(ClientDTO.FromDatabaseUser(client))
            };
        }

        /// <summary>
        /// Get details of specified organizer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("organizers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrganizer(Guid id)
        {
            return repositoryWrapper.OrganizerRepository.GetById(id) switch
            {
                null => NotFound("Organizer not found"),
                Organizer organizer => Ok(OrganizerDTO.FromDatabaseUser(organizer))
            };
        }

        #endregion
    }
}
