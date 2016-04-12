using Egypt.Domain;

namespace Egypt.API.Resources
{
    public class UserRegisterRequest
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Gender Gender { get; set; }
    }
}