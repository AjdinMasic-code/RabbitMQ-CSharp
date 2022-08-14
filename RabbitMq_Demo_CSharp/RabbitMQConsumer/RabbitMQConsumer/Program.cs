using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

var queueName = "SomeQueue";

channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (ch, ea) =>
{
    var body = ea.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message Received: {message} {DateTime.Now}");
};


// this consumer tag identifies the subscription
// when it has to be cancelled
string consumerTag = channel.BasicConsume(queueName, true, consumer);
// ensure we get a delivery
//bool waitRes = latch.WaitOne(2000);
Console.ReadLine();