using bank_app_backend.Models;

namespace bank_app_backend.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public void CreateCustomer(Customer customer);
        public List<Customer> GetAllCustomers();
        public void CreateAccount(Account account);
        public void UpdateCustomer(Customer customer);
    }
}
