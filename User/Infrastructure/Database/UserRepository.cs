using Microsoft.EntityFrameworkCore;
using TutorialRestApi.Core.Database;
using TutorialRestApi.User.App.Models;
using TutorialRestApi.User.App.Repositories;

namespace TutorialRestApi.User.Infrastructure.Database
{
    public class UserRepository : IUserRepository
    {
        private TodoContext _dbContext;
        public UserRepository(TodoContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public UserModel? GetUserByLoginAndPassword(string login, string encryptedPassword)
        {
           return _dbContext.Users.Where( user => user.Username == login && user.Password == encryptedPassword ).FirstOrDefault();
        }

        public void AddUser(UserModel user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public UserModel? GetById(int id)
        {
            return _dbContext.Users.Where(user => user.Id == id).FirstOrDefault();
        }
    }
}
