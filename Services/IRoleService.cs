using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoleService
    {
        public List<Role> GetAllRoles();
        public Role GetRoleById(int id);
        public bool AddRole(Role role);
    }
}
