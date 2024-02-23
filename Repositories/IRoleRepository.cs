using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoleRepository
    {
        public List<Role> GetAllAccounts();
        public Role GetRoleById(int id);
        public bool AddRole(Role role);
    }
}
