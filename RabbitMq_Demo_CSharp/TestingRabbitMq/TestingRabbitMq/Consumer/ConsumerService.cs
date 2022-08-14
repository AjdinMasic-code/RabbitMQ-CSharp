using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingRabbitMq.Consumer
{
    public class ConsumerService
    {
        public async Task ConsumeMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Permaplate_Invoicing",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var queueName = "Permaplate_Invoicing";

            var consumer = new AsyncEventingBasicConsumer(channel);

            string message = "";

            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                    
                message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message Received: {message}");

                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();

            };

            Task.WaitAll();

            Console.WriteLine($"Hopefully it read it: {message}");
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            string consumerTag = channel.BasicConsume(queueName, true, consumer);
            // ensure we get a delivery
            //bool waitRes = latch.WaitOne(2000);
            Console.ReadLine();
            
        }
    }
}
