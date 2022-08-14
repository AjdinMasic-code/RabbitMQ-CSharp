using RabbitMQ.Client;
using System.Text;
using TestingRabbitMq.Consumer;

try
{
    var uInput = "";

    while(uInput != null && uInput != "close" && uInput != "cancel" && uInput != "stop")
    {
        Console.WriteLine("What message would you like to send????????");

        uInput = Console.ReadLine();

        
        var queueName = "SomeQueue";

        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            string message = uInput;
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                    routingKey: queueName,
                                    basicProperties: null,
                                    body: body);
            Console.WriteLine($"Message sent {message} - {DateTime.Now}");

            uInput = uInput.ToLower();
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(Environment.GetEnvironmentVariable("rabbitMqUrl"));
    var tempExcetion = e.InnerException;
    Console.WriteLine(e.Message);
    Console.WriteLine(e.StackTrace);
    while (tempExcetion.InnerException != null)
    {
        Console.WriteLine(tempExcetion.InnerException.Message);
        Console.WriteLine(tempExcetion.InnerException.StackTrace);
        tempExcetion = tempExcetion.InnerException;
    }
    // Console.WriteLine(e.StackTrace);
}