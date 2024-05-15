namespace DevFreela.Core.Services
{
    public interface IMessageBusService
    {
        Task PublishService(string queue, byte[] message);
    }
}
