using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRoleService
    {
        public bool AddRole(Role role) => RoleDAO.Instance.Add(role);

        public List<Role> GetAllRoles() => RoleDAO.Instance.GetAll();

        public Role GetRoleById(int id) => RoleDAO.Instance.GetAll().FirstOrDefault(x => x.RoleId == id);
    }
}
