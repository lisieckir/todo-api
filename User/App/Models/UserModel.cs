using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using TutorialRestApi.Todo.App.Models;

namespace TutorialRestApi.User.App.Models
{
    public class UserModel
    {
        public long Id { get; set; }


        public required string Username { get; set; }

        public string EmailAddress { get; set; }

        private string? _password;

        [JsonIgnore]
        public string? Password
        {
            get
            {
                return this._password;
            }

            set
            {
                SHA256 mySHA256 = SHA256.Create();
                Encoding enc = Encoding.UTF8;

                this._password = Convert.ToBase64String(mySHA256.ComputeHash(enc.GetBytes(value)));
            }
        }

        [JsonIgnore]
        public IList<TodoItem> Todos { get; } = new List<TodoItem>();

    }
}
