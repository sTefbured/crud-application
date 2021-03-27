namespace Lab1.Repository.Exception
{
    public class UserNotFoundException : System.Exception
    {
        private const string defaultMessage = "User not found.";
        
        public UserNotFoundException() : this(defaultMessage){}
        
        public  UserNotFoundException(string message) : base(message){}
    }
}