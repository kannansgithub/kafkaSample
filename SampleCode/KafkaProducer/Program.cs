using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    Console.WriteLine("Producer Started Please enter the state to produce: ");

    string? state;
    while ((state = Console.ReadLine()) != null)
    {
        var response = await producer.ProduceAsync("wether_topic", new Message<Null, string> { Value = JsonSerializer.Serialize(new Wether(state, 70)) });
        Console.WriteLine(response.Value);
    }
}catch(ProduceException<Null,string> ex)
{
    Console.WriteLine(ex.Message);
}


public record Wether(string State, int Temparature);

