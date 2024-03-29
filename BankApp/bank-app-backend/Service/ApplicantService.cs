﻿using bank_app_backend.Models;
using bank_app_backend.Exceptions;
using bank_app_backend.Repositories.Interfaces;
using BankAppBackend.Service.Interfaces;

namespace BankAppBackend.Service
{
    public class ApplicantService : IApplicantService
    {
        private IApplicantRepository applicantRepository;
        private IRedisMessagePublisherService redisMessagePublisherService;
        private ICustomerService customerService;
        //private ITellerService tellerService;
        public ApplicantService(IApplicantRepository applicantRepository, IRedisMessagePublisherService redisMessagePublisherService, ICustomerService customerService)
        {
            this.applicantRepository = applicantRepository;
            this.redisMessagePublisherService = redisMessagePublisherService;
            this.customerService = customerService;
            //this.tellerService = tellerService;
        }

        public Applicant AddApplicant(Applicant applicant)
        {
            //Teller teller = this.tellerService.GetTellerById(applicant.TellerId);
            if (applicantRepository.FindApplicantByCNIC(applicant.CNIC) != null)
            {
                throw new EntityAlreadyExistException($"Applicant already exist with CNIC number : {applicant.CNIC}");
            }
            else if (applicantRepository.FindApplicantByEmailAddress(applicant.EmailAddress) != null)
            {
                throw new EntityAlreadyExistException($"Applicant already exist with email address : {applicant.EmailAddress}");
            }
            applicant.AccountStatus = AccountStatuses.PENDING;
            //applicant.Teller = teller;
            applicantRepository.AddApplicant(applicant);
            return applicant;
        }

        public List<Applicant> GetAllApplicantList()
        {
            return applicantRepository.GetApplicants().ToList();
        }

        public Applicant GetApplicantByEmail(string applicantEmail)
        {
            Applicant? applicant = applicantRepository.FindApplicantByEmailAddress(applicantEmail);
            if (applicant == null)
            {
                throw new EntityNotFoundException($"Applicant not found with applicant email address :{applicantEmail}");
            }
            return applicant;
        }

        public Applicant GetApplicantById(long applicantId)
        {
            Applicant? applicant = applicantRepository.findApplicantById(applicantId);
            if (applicant == null)
            {
                throw new EntityNotFoundException($"Applicant not found with applicant id :{applicantId}");
            }
            return applicant;
        }

        public Applicant? GetApplicantDetailsFromCredentials(string emailAddress, string password)
        {
            Applicant? applicant = applicantRepository.FindApplicantByEmailAddress(emailAddress);
            if (applicant == null)
            {
                throw new EntityNotFoundException($"User not found with email address {emailAddress}");
            }
            else if (applicant.Customer == null)
            {
                throw new EntityNotFoundException($"Your applicant request is not approved by bank officials yet");
            }
            else if (applicant.Customer.Password.Equals(password))
            {
                return applicant;
            }

            return null;
        }

        public void UpdateApplicantStatus(long applicantId, AccountStatuses accountStatus, Teller teller)
        {
            Applicant? applicant = GetApplicantById(applicantId);

            if (applicant == null)
            {
                throw new EntityNotFoundException($"Applicant with id {applicantId} not found");
            }

            if (customerService.CheckIfCustomerExistAgainstApplicantId(applicantId))
            {
                throw new EntityAlreadyExistException($"Customer already exist against this applicant id {applicantId}");
            }

            applicant.Teller = teller;
            applicant.AccountStatus = accountStatus;

            applicantRepository.UpdateApplicant(applicant);
            if (applicant.AccountStatus.Equals(AccountStatuses.APPROVED))
            {
                customerService.CreateCustomerAndAccount(applicant);
            }

            //preparing model to send on queue for another MVC App.
            ApplicantMessagesModel applicantMessageModel = new ApplicantMessagesModel()
            {
                ApplicantId = applicant.Id,
                AccountStatus = accountStatus,
                ApplicantEmailAddress = applicant.EmailAddress,
                Message = $"Dear Applicant {applicant.ApplicateName}, your status has been updated to {accountStatus}"
            };

            redisMessagePublisherService.SendMessage(applicantMessageModel);
        }
    }
}
