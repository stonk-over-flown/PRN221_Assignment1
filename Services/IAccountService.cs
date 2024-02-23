using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        public bool CheckLogin(string username, string password);
        public List<Account> GetAllAccounts();
        public Account GetAccountById(int id);
        public bool AddAccount(Account account);
        public bool UpdateAccount(Account account);
        public bool DeleteAccount(int id);
    }
}
