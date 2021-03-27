using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Attribute
{
    public class SecurityAttribute : System.Attribute
    {
        private readonly HashSet<UserRole> _roles;

        public SecurityAttribute()
        {
            _roles = new HashSet<UserRole>();
        }

        public SecurityAttribute(params UserRole[] roles) : this()
        {
            foreach (var role in roles)
            {
                _roles.Add(role);
            }
        }
    }
}