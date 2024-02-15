using bank_app_backend.Models;

namespace bank_app_backend.Repositories.Interfaces
{
    public interface IApplicantRepository
    {
        public Applicant AddApplicant(Applicant applicant);
        public IEnumerable<Applicant> GetApplicants();
        public Applicant? findApplicantById(long applicantId);
        public void UpdateApplicant(Applicant applicant);
        public Applicant? FindApplicantByCNIC(string cnic);
        public Applicant? FindApplicantByEmailAddress(string emailAddress);
    }
}
