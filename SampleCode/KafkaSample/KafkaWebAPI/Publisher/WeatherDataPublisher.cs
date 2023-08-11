using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaWebAPI.Publisher
{
    public class WeatherDataPublisher: IWeatherDataPublisher
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IConfiguration _configuration;

        public WeatherDataPublisher(IProducer<Null,string> producer, IConfiguration configuration)
        {
            _producer=producer;
            _configuration = configuration;
        }
        public async Task ProduceWether(Wether wether)
        {
          await  _producer.ProduceAsync(_configuration.GetValue<string>("topic_name"), new Message<Null, string> {Value= JsonSerializer.Serialize(wether) });
        }
    }
    public record Wether(string State, int Temparature);

}
