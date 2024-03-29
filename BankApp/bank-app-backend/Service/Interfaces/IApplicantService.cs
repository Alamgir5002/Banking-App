﻿using bank_app_backend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface IApplicantService
    {
        public Applicant AddApplicant(Applicant applicant);
        public List<Applicant> GetAllApplicantList();   // its performance will degrade with more data; why we need this
        public Applicant GetApplicantById(long applicantId);
        public void UpdateApplicantStatus(long applicantId, AccountStatuses accountStatus, Teller teller);
        public Applicant? GetApplicantDetailsFromCredentials(string emailAddress, string password);
        public Applicant GetApplicantByEmail(string applicantEmail);
    }
}
