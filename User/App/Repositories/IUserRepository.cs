using TutorialRestApi.User.App.Models;

namespace TutorialRestApi.User.App.Repositories
{
    public interface IUserRepository
    {
        public UserModel? GetUserByLoginAndPassword(string login, string password);
        public void AddUser(UserModel user);

        public UserModel? GetById(int id);
    }
}
