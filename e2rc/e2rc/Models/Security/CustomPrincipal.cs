using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
namespace e2rc.Models.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
          //  return role == Role ? true : false;
            return role.Contains(Role);
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public long? User_ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string LogoPath { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public long? User_ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string LogoPath { get; set; }
    }
}