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

        //public MessageBroker ()
        //{
        //    var config = new ConfigurationBuilder()
        //            .AddJsonFile("appsettings.json")
        //            .Build();

        //    config.GetSection("RabbitMqConnection").Bind(factory);
        //}

        public void PublishMessage(CustomMessage customMessage)
        {

            //factory.HostName = "172.20.20.23";
            //factory.UserName = "guest";
            //factory.Password = "guest";
            //factory.VirtualHost = "/";
            //factory.AutomaticRecoveryEnabled = true;
            //factory.RequestedHeartbeat = 30;

            //using (var conn = factory.CreateConnection())
            //{
            //    using(var Channel = conn.CreateModel())
            //    {
                    
            //        Channel.ExchangeDeclare(exchange: EXCHANGENAME, type: "topic", durable: true);
            //        Channel.QueueDeclare(QUEUENAME, true, false, false, null);
            //        Channel.QueueBind(QUEUENAME, EXCHANGENAME, ROUTING_KEY);

                    
            //        var message = JsonConvert.SerializeObject(customMessage);
            //        var bytes = Encoding.ASCII.GetBytes(message);
            //        Channel.BasicPublish(EXCHANGENAME, ROUTING_KEY, null, bytes);
            //    }
            //}

            
        }
    }
}
