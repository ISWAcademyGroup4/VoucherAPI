using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using RabbitMQ.Client;

namespace VoucherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            //Setup NLog to catch all errors
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();  
            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped Program because of exception");
                throw;
            }
            finally
            {
                //Flush and stop internal timers and threads before application exit
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddEventSourceLogger();
                })
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {

                })     
                .UseStartup<Startup>().UseNLog()
                .UseUrls("https://172.20.20.23:5001","http://172.20.20.23:5000");
    }
}
