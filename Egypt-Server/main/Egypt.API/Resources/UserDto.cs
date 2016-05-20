using Egypt.Domain;

namespace Egypt.API.Resources
{
    public class UserDto
    {
        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Gender = user.Gender;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; } 
    }
}