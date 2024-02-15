using bank_app_backend.Models;
using BankAppBackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly IRedisMessagePublisherService RedisMessagePublisherService;

        public TrackingController(IRedisMessagePublisherService redisMessagePublisherService)
        {
            RedisMessagePublisherService = redisMessagePublisherService;
        }

        /// <summary>
        /// Sends a message to the "bank-account-status-changed-channel" channel on Redis. (Built for interactive testing purposes)
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        [HttpPost]
        public async Task SendMessage([FromBody] ApplicantMessagesModel message)
        {
            await RedisMessagePublisherService.SendMessage(message);
        }
    }
}
