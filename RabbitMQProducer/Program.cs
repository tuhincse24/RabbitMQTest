using System;

namespace RabbitMQProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Topicmessages topicmessages = new Topicmessages();
            for(int i = 0; i < 20000; i++)
            {
                topicmessages.SendMessage();
            }
            Console.ReadLine();
        }
    }
}
