using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Services
{
    public class RabbitMQService
    {

        private static IConfiguration Configuration;

        public RabbitMQService(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public static IConfigurationRoot ConfigurationRoot()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


    }
}
