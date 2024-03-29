﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bank_app_backend.Models
{
    public enum AccountTypes
    {
        CURRENT, SAVING, BUSINESS // we dont like all caps names in general; its considered YELLING
    }
    public enum AccountStatuses
    {
        APPROVED, DENIED, PENDING
    }

    public class Applicant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "Applicant name can't be empty")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name length should be between 2-25 characters")]
        public string ApplicateName { get; set; }
        [Required(ErrorMessage = "Applicant address can't be empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Applicant phone number can't be empty")]
        public string CNIC { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Applicant email can't be empty")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Applicant dob can't be empty")]
        public DateOnly DOB { get; set; }
        [Required(ErrorMessage = "Applicant account type can't be null")]
        public AccountTypes AccountType { get; set; }
        public AccountStatuses AccountStatus { get; set; } = AccountStatuses.PENDING;
        public long? TellerId { get; set; } //teller foreign key
        public Teller? Teller { get; set; } // navigation property 
        public Customer? Customer { get; set; }
    }
}
