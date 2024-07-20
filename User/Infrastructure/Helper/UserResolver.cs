using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using TutorialRestApi.User.App.Models;
using TutorialRestApi.User.App.Repositories;

namespace TutorialRestApi.User.Infrastructure.Helper
{
    public class UserResolver
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolver(IUserRepository userRepository, IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
            _userRepository = userRepository;
        }

        public UserModel? Resolve()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value;

            if (userId == null)
            {
                return null;
            }

            return _userRepository.GetById(Int32.Parse(userId));
        }
    }
}
