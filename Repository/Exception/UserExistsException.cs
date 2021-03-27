namespace Lab1.Repository.Exception
{
    public class UserExistsException : System.Exception
    {
        private const string defaultMessage = "User with such login already exists.";
        
        public UserExistsException() : this(defaultMessage) {}

        public UserExistsException(string message) : base(message) {}
    }
}