﻿using bank_app_backend.Models;
using bank_app_backend.Exceptions;
using bank_app_backend.Repositories.Interfaces;
using BankAppBackend.Service.Interfaces;

namespace BankAppBackend.Service
{
    public class TellerService : ITellerService
    {
        private IApplicantService applicantService;
        private ITellerRepository tellerRepository;

        public TellerService(IApplicantService applicantService, ITellerRepository tellerRepository)
        {
            this.applicantService = applicantService;
            this.tellerRepository = tellerRepository;
        }
        
        public void ChangeApplicantStatus(long applicantId, AccountStatuses accountStatus, long tellerId)
        {
            Teller? teller = tellerRepository.GetTellerById(tellerId);
            if (teller == null)
            {
                throw new EntityNotFoundException($"Teller not found with id: {tellerId}");
            }
            applicantService.UpdateApplicantStatus(applicantId, accountStatus, teller);
        }

        public Teller? GetTellerById(long id)
        {
            Teller? teller = this.tellerRepository.GetTellerById(id);
            if (teller == null)
            {
                throw new EntityNotFoundException($"Teller not found with teller id {id}");
            }
            return teller;
        }

        public Teller? GetTellerDetailsByLoginCredentials(string emailAddress, string password)
        {
            return tellerRepository.GetTellerDetailsByLoginCredentials(emailAddress, password);
        }

        public Teller RegisterTeller(Teller teller)
        {
            if (tellerRepository.GetTellerByEmailAddress(teller.EmailAddress) != null)
            {
                throw new EntityAlreadyExistException($"Teller already exist with email address {teller.EmailAddress}");
            }
            return tellerRepository.RegisterTeller(teller);
        }
    }
}
