using bank_app_backend.Models;

namespace bank_app_backend.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Account CreateAccount(Account account);
        public Account? GetAccountById(Guid id);
        public List<Account> GetAccountsAgainstCustomerId(long customerId);
        public Account GetAccountAgainstCustomerId(long customerId);
    }
}
