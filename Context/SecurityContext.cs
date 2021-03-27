using System;
using System.Reflection;
using Lab1.Attribute;
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
            CurrentUser = new User("", "", UserRole.GUEST);
        }

        public bool IsAccessible(object value, string methodName)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(methodName);
            var attribute = (SecurityAttribute) memberInfo[0]
                .GetCustomAttribute(typeof(SecurityAttribute), false);
            return (attribute != null)
                   && attribute.Roles.Contains(CurrentUser.Role);
        }
    }
}