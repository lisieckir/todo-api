using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TutorialRestApi.User.App.Models;
using TutorialRestApi.User.App.Repositories;
using TutorialRestApi.User.Infrastructure.Dtos;
using System.Security.Cryptography;
using System.Security.Claims;

namespace TutorialRestApi.User.Infrastructure.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;

        public JWTController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthorizationDTO login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: new Claim[]
              {
                  new Claim(ClaimTypes.Actor, userInfo.Id.ToString())
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel? AuthenticateUser(AuthorizationDTO login)
        {
            SHA256 mySHA256 = SHA256.Create();
            Encoding enc = Encoding.UTF8;

            System.Byte[] password = mySHA256.ComputeHash(enc.GetBytes(login.Password));
            return this._userRepository.GetUserByLoginAndPassword(login.Username, Convert.ToBase64String(password));
        }
    }
}
