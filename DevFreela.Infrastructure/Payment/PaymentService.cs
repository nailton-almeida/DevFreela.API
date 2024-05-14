using DevFreela.Core.DTO;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private readonly IConfiguration _configuration;
        public PaymentService(IConfiguration configuration, IMessageBusService messageBusService)
        {
            _configuration = configuration;
            _messageBusService = messageBusService;
        }

        public async Task CheckPaymentAsync(PaymentProjectDTO paymentDTO)
        {
            var queueName = _configuration["RabbitQueues:PAYMENT_PROCESSING"];

            var paymentJson = JsonSerializer.Serialize(paymentDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentJson);

            await _messageBusService.PublishService(queueName, paymentInfoBytes);
        }
    }
}
