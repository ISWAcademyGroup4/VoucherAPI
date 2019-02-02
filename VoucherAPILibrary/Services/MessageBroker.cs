using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Services
{
    public class MessageBroker
    {
        const string ROUTING_KEY = "my.queue.key";
        const string EXCHANGENAME = "my_queue_exchange";
        const string QUEUENAME = ROUTING_KEY;

        private IModel channel;

        public MessageBroker()
        {
            
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.HostName = "127.0.0.1";
            factory.Port = 5672;
            factory.VirtualHost = "/";

            IConnection connection = factory.CreateConnection();
            Console.WriteLine("Rabbit MQ Connection has been initiated!");

            channel = connection.CreateModel();
            Console.WriteLine("Rabbit MQ Channel has been created");

            channel.ExchangeDeclare(exchange: EXCHANGENAME, type: "topic", durable: true);
            channel.QueueDeclare(QUEUENAME, true, false, false, null);
            channel.QueueBind(QUEUENAME, EXCHANGENAME, ROUTING_KEY);
            
        }

        public void Send(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(EXCHANGENAME, ROUTING_KEY, null, body);
        }
    }
}
