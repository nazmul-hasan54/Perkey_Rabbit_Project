using Domain.Models;
using InterfacesToContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWrapperRepository _wrapperRepo;
        public UsersController(IWrapperRepository wrapperRepo)
        {
            _wrapperRepo = wrapperRepo;
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _wrapperRepo.Users.All();
            return Ok(users);
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var users = await _wrapperRepo.Users.GetById(id);
            return Ok(users);
        }
        [HttpPost("user-register")]
        public async Task<IActionResult> AddUser(Users user)
        {
            var userExists = await _wrapperRepo.Users.GetUserByEmail(user.Email);
            if (userExists != null)
            {
                return BadRequest("User is already exists");
            }
            else 
            {
                try
                {
                    var creteUser = await _wrapperRepo.Users.Add(user);
                    return Ok(creteUser);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpGet("update-user")]
        public async Task<IActionResult> UpdateUser(int id, Users users) 
        {
            try
            {
                var userId = await _wrapperRepo.Users.GetById(id);
                if (userId != null)
                    return NotFound();
                await _wrapperRepo.Users.Update(id,users);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
