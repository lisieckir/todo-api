using Microsoft.AspNetCore.Mvc;
using TutorialRestApi.User.App.Models;
using TutorialRestApi.User.App.Repositories;
using TutorialRestApi.User.Infrastructure.Dtos;

namespace TutorialRestApi.User.Infrastructure.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult registerAction([FromBody] SignupDTO signupDTO, [FromServices] IUserRepository userRepository)
        {
            UserModel user = new UserModel
            {
                Username = signupDTO.UserName,
                Password = signupDTO.Password,
                EmailAddress = signupDTO.EmailAddress
            };

            userRepository.AddUser(user);
            return Ok();
        }
    }
}
