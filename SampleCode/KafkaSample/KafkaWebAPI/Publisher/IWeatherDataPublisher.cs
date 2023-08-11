namespace KafkaWebAPI.Publisher
{
    public interface IWeatherDataPublisher
    {
        Task ProduceWether(Wether wether);
    }
}