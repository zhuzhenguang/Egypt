namespace Egypt.Domain
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email, string password, Gender gender)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Gender Gender { get; set; }

        public virtual dynamic ToDto(User user)
        {
            return new 
            {
                name = user.Name,
                email = user.Email,
                password = user.Password,
                gender = user.Gender
            };
        }
    }
}