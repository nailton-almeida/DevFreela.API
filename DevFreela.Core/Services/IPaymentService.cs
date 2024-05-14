
using DevFreela.Core.DTO;

namespace DevFreela.Core.Services;
public interface IPaymentService
{
    public Task CheckPaymentAsync(PaymentProjectDTO payment);
}