# RabbitMQ-C#
RabbitMQ demo with C#

This code base uses C# to demo RabbitMQ. For this demo you will need Docker installed and some basic knowledge of docker commands if you are using Windows. For this project to work you will also need Visual Studio 2022. The edition doesn't matter.

This demo was developed on Windows using Docker.

<a href="https://www.docker.com/products/docker-desktop/" title="Docker Desktop Link">Docker Desktop Link </a>

After installing docker run the below command to install RabbitMQ and set up a port to run the server on.
<br />
docker run -d --hostname ppRabbit --name ppRabbit -p 15672:15672 -p  5672:5672 rabbitmq:3-management

For more details about what this line of code does:
<a href="https://hub.docker.com/_/rabbitmq" Title="RabbitMQ details link">Rabbit MQ Documentation with Docker</a>

To test and see if you set it up correctly:

Go to localhost:15672, at this point you should see the RabbitMQ Management Console.
If you have docker desktop installed you should see a container running.


To successfully test this:
You need to run the console applications at the same time in separate Visual Studio instances.
RabbitMQConsumer is the consumer 
TestingRabbitMq is the sender/producer

If all of the previous steps were followed, these 2 services should set up your queues if they already don't exist.

After running both applications in separate Visual Studio instances go back to the RabbitMQ Management Console at localhost:15672. In the navigation bar at the top you should see Overview, Connections, Channels, Exchanges, Queues, Admin. Click on Queues. Make sure "All Queues" is expanded, in there you should a queue called "SomeQueue". Additionally, if you have the consumer running, you should see the consumer under "Consumer" under the "Queue" tab.

If you update the queue names in the sender/producer and consumer, you may need to clean the solution. I had issues where I was targetting an old queue name and SomeQueue was not receiving any messages and they were going to the old queue name.



