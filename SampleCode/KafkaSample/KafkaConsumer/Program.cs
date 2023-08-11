using Confluent.Kafka;
using System.Text.Json;

var config= new ConsumerConfig
    {
    BootstrapServers= "localhost:9092",
    GroupId="weather_consumer_group",
    AutoOffsetReset=AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();

consumer.Subscribe("wether_topic");

CancellationTokenSource cts = new CancellationTokenSource();
try
{
    while (true)
    {
        var response=consumer.Consume(cts.Token);
        if(response?.Message != null)
        {
            var wether = JsonSerializer.Deserialize<Wether>(response.Message.Value);
            Console.WriteLine($"Received Wether for the City {wether?.State}, with the Temp: {wether?.Temparature}F ");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

public record Wether(string State, int Temparature);
