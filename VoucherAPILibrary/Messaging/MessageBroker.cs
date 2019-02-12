using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using VoucherAPILibrary.Messaging;


namespace VoucherAPILibrary.Messaging
{
    public class MessageBroker
    {
        private static ConnectionFactory factory = new ConnectionFactory();

        const string ROUTING_KEY = "net.voucherz.voucherapi";
        const string EXCHANGENAME = "net_voucherz_exchange";
        const string QUEUENAME = ROUTING_KEY;

        public void PublishMessage(CustomMessage customMessage)
        {
            factory.HostName = "192.168.43.55";
            factory.UserName = "server";
            factory.Password = "admin";
            factory.VirtualHost = "/";
            factory.AutomaticRecoveryEnabled = true;
            factory.RequestedHeartbeat = 30;

            try
            {
                using (var conn = factory.CreateConnection())
                {
                    Console.WriteLine("Connection to Audit Service::Initiated");
                    using (var Channel = conn.CreateModel())
                    {
                        Channel.ExchangeDeclare(exchange: EXCHANGENAME, type: "topic", durable: true);
                        Channel.QueueDeclare(QUEUENAME, true, false, false, null);
                        Channel.QueueBind(QUEUENAME, EXCHANGENAME, ROUTING_KEY);

                        var message = JsonConvert.SerializeObject(customMessage);
                        var bytes = Encoding.ASCII.GetBytes(message);
                        Channel.BasicPublish(EXCHANGENAME, ROUTING_KEY, null, bytes);
                        Console.WriteLine("Successfully published to Audit Service");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Publish call failed due to {0}",ex.Message);
            }
            


            
        }
    }
}
