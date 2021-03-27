using Lab1.Models;

namespace Lab1.Context
{
    public class SecurityContext
    {
        private static SecurityContext _instance;
        public static SecurityContext Instance => _instance ??= new SecurityContext();
        
        public User CurrentUser { get; set; }

        private SecurityContext()
        {
            
        }
        
        
    }
}