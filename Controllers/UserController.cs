using Buscador.Dtos;
using Buscador.Dtos.Requests;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Buscador.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UserController : ControllerBase
    {
        private readonly IUserAplication _userAplication;

        public UserController(IUserAplication userAplication)
        {
            _userAplication = userAplication;
        }

        /// <summary>
        /// Realiza a pesquisa de usuario por ID.
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetId(int userId)
        {

            var user = await _userAplication.GetUserNameAsync(userId);

            return Ok(user);
        }

        /// <summary>
        /// Realiza a criação de usuario.
        /// </summary>
        // POST: UserController/Details/5
        [HttpPost("")]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            var data = await _userAplication.CreateUserAsync(user);

            return CreatedAtAction(nameof(GetId), new { userId = data.Id }, data);
        }

        /// <summary>
        /// Define Admin.
        /// </summary>
        // PATCH: UserController/Details/5
        [HttpPatch("/{idUser}/admin/{isAdmin}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAdmin([FromRoute] int idUser, bool isAdmin)
        {
            var success = await _userAplication.UpdateAdminAsync(idUser, isAdmin);

            return NoContent();
        }

        /// <summary>
        /// Define o status do usuario.
        /// </summary>
        // PATCH: UserController/Details/5
        [HttpPatch("/{idUser}/status/{active}")]
        [ProducesResponseType(typeof(UserResponse), 204)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SetUserStatusAsync([FromRoute] int idUser, bool active)
        {

            var success = await _userAplication.SetUserStatusAsync(idUser, active);
            return NoContent();
        }

        /// <summary>
        /// Valida Login.
        /// </summary>
        // POST: UserController
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            bool success = await _userAplication.LoginAsync(loginRequest);
            if (!success)
            {
                return Unauthorized(new { error = "Credenciais inválidas." });
            }

            return Ok(new { message = "Login realizado com sucesso." });
        }
    }
}
