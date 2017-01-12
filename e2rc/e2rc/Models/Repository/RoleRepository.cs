using System.Collections.Generic;
using e2rcModel.BusinessLayer;

namespace e2rc.Models.Repository
{
    public static class RoleRepository
    {
        public static string Role_Type { get; set; }

        public static IEnumerable<RoleModel> Roles
        {
            get
            {
                Role.Role_Type = Role_Type;
                List<RoleModel> roles = new List<RoleModel>();
                foreach (var role in new Role().Roles)
                {
                    roles.Add(new RoleModel { Description = role.Description, Role_ID = role.Role_ID });
                }
                return roles;
            }
        }
    }
}