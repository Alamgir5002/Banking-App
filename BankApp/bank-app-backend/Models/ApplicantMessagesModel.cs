﻿namespace bank_app_backend.Models
{
    public class ApplicantMessagesModel
    {
        public Guid Id { get; set; }
        public long ApplicantId { get; set; }
        public string? Message { get; set; }
        public string ApplicantEmailAddress { get; set; }

        public AccountStatuses AccountStatus { get; set; }
    }
}
