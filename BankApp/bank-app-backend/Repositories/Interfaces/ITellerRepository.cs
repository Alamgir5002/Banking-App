using bank_app_backend.Models;

namespace bank_app_backend.Repositories.Interfaces
{
    public interface ITellerRepository
    {
        public Teller? GetTellerById(long tellerId);
        public Teller RegisterTeller(Teller teller);
        public Teller? GetTellerByEmailAddress(string emailAddress);
        public Teller? GetTellerDetailsByLoginCredentials(string emailAddress, string password);
    }
}
