namespace SharedModel
{
    public interface MessageConsumer
    {
        string token { get; set; }
        bool status { get; set; }
        string message { get; set; }
    }
}
