

using Confluent.Kafka;

namespace KafkaProducer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            string brokerList = "localhost:9092";
            string topicName = "test-topic";

            var config = new ProducerConfig { BootstrapServers = brokerList };
            while (true)
            {


                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {

                    string key = "someKey";
                    string val = "someValue";

                    try
                    {
                        // Note: Awaiting the asynchronous produce request below prevents flow of execution
                        // from proceeding until the acknowledgement from the broker is received (at the
                        // expense of low throughput).
                        var deliveryReport = await producer.ProduceAsync(
                            topicName, new Message<string, string> { Key = key, Value = val });

                        Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                    }
                    catch (ProduceException<string, string> e)
                    {
                        Console.WriteLine($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                    }
                }

                await Task.Delay(500);
            }
        }
    }

}
