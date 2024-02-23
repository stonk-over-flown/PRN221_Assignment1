using BO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(int id) => AccountDAO.Instance.GetAll().FirstOrDefault(x => x.AccountId == id);

        public List<Account> GetAllAccounts() => AccountDAO.Instance.GetAll();

        public bool AddAccount(Account account) => AccountDAO.Instance.Add(account);

        public bool UpdateAccount(Account account) => AccountDAO.Instance.Update(account, GetAccountById(account.AccountId));

        public bool DeleteAccount(int id) => AccountDAO.Instance.Delete(GetAccountById(id));
    }
}
