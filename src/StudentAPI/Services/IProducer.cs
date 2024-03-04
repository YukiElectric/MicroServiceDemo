namespace StudentAPI.Services
{
    public interface IProducer
    {
        public void sendMessage<T>(T message);
    }
}
