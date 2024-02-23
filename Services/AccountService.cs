using BO;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository = null;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public bool AddAccount(Account account) => _accountRepository.AddAccount(account);

        public bool CheckLogin(string email, string password) => _accountRepository.GetAllAccounts().Any(x => x.AccountEmail == email && x.AccountPassword == password);

        public bool DeleteAccount(int id) => _accountRepository.DeleteAccount(id);

        public Account GetAccountById(int id) => _accountRepository.GetAccountById(id);

        public List<Account> GetAllAccounts() => _accountRepository.GetAllAccounts();

        public bool UpdateAccount(Account account) => _accountRepository.UpdateAccount(account);
    }
}
