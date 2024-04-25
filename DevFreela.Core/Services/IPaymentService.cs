namespace DevFreela.Core.Services;

public interface IPaymentService
{
    public Task<bool> CheckPaymentAsync();
}