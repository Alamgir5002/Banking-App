using bank_app_backend.Models;

namespace bank_app_backend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        public Transaction AddTransaction(Transaction transaction);
        public IEnumerable<Transaction> GetTransactions();
        public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId);
        public Transaction? GetTransactionById(Guid id);
    }
}
