using System;
using System.Linq;
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

        private bool IsAccessible(MemberInfo method)
        {
            var attribute = (SecurityAttribute) method
                .GetCustomAttribute(typeof(SecurityAttribute), false);
            return (attribute != null)
                   && attribute.Roles.Contains(CurrentUser.Role);
        }

        public object Invoke(object controller, string methodName, params object[] parameters)
        {
            var type = controller.GetType();
            var method = GetMethod(type, methodName, parameters);
            if (method != null && IsAccessible(method))
            {
                return method.Invoke(controller, parameters);
            }

            return null;
        }

        private MethodInfo GetMethod(Type type, string methodName, params object[] parameters)
        {
            return type.GetMethods()
                        .FirstOrDefault(m =>
            {
                if (!m.Name.Equals(methodName) || m.GetParameters().Length != parameters.Length)
                {
                    return false;
                }

                return !parameters.Where((t, i) => t.GetType() != m.GetParameters()[i].ParameterType).Any();
            });
        }
    }
}