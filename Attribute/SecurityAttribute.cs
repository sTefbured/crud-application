using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SecurityAttribute : System.Attribute
    {
        public HashSet<UserRole> Roles { get; }

        public SecurityAttribute(params UserRole[] roles)
        {
            Roles = new HashSet<UserRole>();
            foreach (var role in roles)
            {
                Roles.Add(role);
            }
        }
    }
}