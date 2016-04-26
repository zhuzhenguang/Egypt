using Egypt.Domain;

namespace Egypt.API.Resources
{
    public class UserRegisterRequest
    {
        public UserRegisterRequest()
        {
        }

        public UserRegisterRequest(string name, string email, string password, Gender gender)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }

        public bool AnyBlank()
        {
            return ObjectExtention.AnyBlank(
                Name,
                Email,
                Password,
                Gender);
        }
    }
}