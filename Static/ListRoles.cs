using FurrLife.Models;
using System.Collections.Generic;

namespace FurrLife.Static
{
    public class ListRoles
    {
        public List<UserRoles> Roles { get; set; }
        public ListRoles()
        {
            Roles = new List<UserRoles>
            {
                UserRoles.Administrator,
                UserRoles.Veterinarian,
                UserRoles.Staff,
                UserRoles.Customer
            };
        }
    }
}
