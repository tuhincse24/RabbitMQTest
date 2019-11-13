using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    class Program
    {
        private const string UName = "guest";
        private const string Pwd = "guest";
        private const string HName = "localhost";
        static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HName,
                UserName = UName,
                Password = Pwd,
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 100, global: false);
            channel.BasicConsume("topic.dhaka.queue", false, consumer);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body;
                Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body)));
                await Task.Run(() => Console.WriteLine(string.Concat("Delivery Tag: ", ea.DeliveryTag)));
                Thread.Sleep(1000);
                Console.WriteLine(string.Concat("Delivery Tag: ", ea.DeliveryTag));
                String consumerTag = channel.BasicConsume("topic.dhaka.queue", false, consumer);
                Console.WriteLine(string.Concat("Consumer Tag: ", consumerTag));
                // ... process the message
                channel.BasicAck(ea.DeliveryTag, false);
            };

            //// accept only one unack-ed message at a time
            //// uint prefetchSize, ushort prefetchCount, bool global
            //channel.BasicQos(0, 10, false);
            //MessageReceiver messageReceiver = new MessageReceiver(channel);
            //channel.BasicConsume("topic.dhaka.queue", false, messageReceiver);
            Console.ReadLine();
        }
    }
}

