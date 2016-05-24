namespace Egypt.API.Resources
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLoginRequest()
        {
        }

        public UserLoginRequest(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        
    }
}