﻿using bank_app_backend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface ITellerService
    {
        public void ChangeApplicantStatus(long applicantId, AccountStatuses accountStatus, long tellerId);
        public Teller? GetTellerById(long id);
        public Teller RegisterTeller(Teller teller);
        public Teller? GetTellerDetailsByLoginCredentials(string emailAddress, string password);
    }
}
