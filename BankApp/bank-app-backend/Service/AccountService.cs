using BankAppBackend.Service.Interfaces;
using bank_app_backend.Models;
using bank_app_backend.Exceptions;
using bank_app_backend.Repositories.Interfaces;

namespace BankAppBackend.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public Account CreateNewAccount(Customer customer)
        {
            Account account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountType = customer.Applicant.AccountType,
                Customer = customer
            };

            account = _accountRepository.CreateAccount(account);
            return account;
        }

        public Account GetAccountAgainstCustomerId(long customerId)
        {
            return _accountRepository.GetAccountAgainstCustomerId(customerId);
        }

        public Account GetAccountById(Guid id)
        {
            Account account = _accountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new EntityNotFoundException($"Account does not exist with account id : {id}");
            }
            return account;
        }

        public List<Account> GetAccountsAgainstCustomerId(long customerId)
        {
            return _accountRepository.GetAccountsAgainstCustomerId(customerId);
        }
    }
}
