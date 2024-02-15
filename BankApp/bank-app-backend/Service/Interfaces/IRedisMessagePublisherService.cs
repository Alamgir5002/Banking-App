using bank_app_backend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface IRedisMessagePublisherService
    {
        public Task SendMessage(ApplicantMessagesModel applicantMessagesModel);
    }
}
