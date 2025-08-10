using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBACAssignment;

namespace RbacBackend.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]  // Only Admin can access these user management APIs
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/admin/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll()
                .Select(u => new { u.Username, u.Role });
            return Ok(users);
        }

        // POST: api/admin/users
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Role))
            {
                return BadRequest("Username, Password, and Role are required.");
            }

            var added = _userService.AddUser(user);
            if (!added)
                return Conflict("User already exists.");

            return CreatedAtAction(nameof(GetAllUsers), new { username = user.Username }, user);
        }

        // PUT: api/admin/users/{username}
        [HttpPut("{username}")]
        public IActionResult EditUser(string username, [FromBody] User updatedUser)
        {
            if (username != updatedUser.Username)
                return BadRequest("Username mismatch.");

            var existingUser = _userService.GetAll().FirstOrDefault(u => u.Username == username);
            if (existingUser == null)
                return NotFound("User not found.");

            existingUser.Password = updatedUser.Password;
            existingUser.Role = updatedUser.Role;

            var updated = _userService.UpdateUser(existingUser);
            if (!updated)
                return StatusCode(500, "Failed to update user.");

            return NoContent();
        }

        // DELETE: api/admin/users/{username}
        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            var deleted = _userService.DeleteUser(username);
            if (!deleted)
                return NotFound("User not found.");

            return NoContent();
        }
    }
}
