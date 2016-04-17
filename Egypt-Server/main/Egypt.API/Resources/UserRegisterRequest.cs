using Egypt.Domain;

namespace Egypt.API.Resources
{
    public class UserRegisterRequest
    {
        public UserRegisterRequest(string name, string email, string password, Gender gender)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Gender Gender { get; private set; }
    }
}