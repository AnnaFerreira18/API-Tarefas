using Application.Interface;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User userWithPassword)
        {
            if (userWithPassword == null)
            {
                return BadRequest("User is null");
            }

            try
            {
                // Separar o usuário e a senha
                var password = userWithPassword.Senha;
                userWithPassword.Senha = null; // Remover a senha do objeto User

                if (string.IsNullOrEmpty(password))
                {
                    return BadRequest("Password is required");
                }

                var existingUser = await _userService.GetUserByUsernameAsync(userWithPassword.Username);
                if (existingUser != null)
                {
                    return Conflict("Username already exists");
                }

                await _userService.RegisterUserAsync(userWithPassword, password);
                return CreatedAtAction(nameof(GetUser), new { id = userWithPassword.Id }, userWithPassword);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(); // Retorna 401 se as credenciais estiverem incorretas
            }

            return Ok(user); // Retorna o usuário autenticado
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("User is null or ID mismatch");
            }

            try
            {
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
