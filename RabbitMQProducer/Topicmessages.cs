using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQProducer
{
    public class Topicmessages
    {
        private const string UName = "guest";
        private const string PWD = "guest";
        private const string HName = "localhost";
        public void SendMessage(int msgCount)

        {
            //Main entry point to the RabbitMQ .NET AMQP client
            var connectionFactory = new ConnectionFactory
            {
                UserName = UName,
                Password = PWD,
                HostName = HName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            var body = $"Message from Topic Exchange 'dhaka' {msgCount}";
            byte[] messagebuffer = Encoding.Default.GetBytes(body);
            model.BasicPublish("topic.exchange", ".dhaka.", properties, messagebuffer);
            Console.WriteLine("Message Sent From: topic.exchange ");

            Console.WriteLine("Routing Key: Message.dhaka.Email");

            Console.WriteLine("Message Sent");

        }

    }
}
