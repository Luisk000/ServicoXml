using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using SerilogConfiguration;
using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace ServicoXml
{
    public class Program
    {
        private static SerilogConfig log = new SerilogConfig();
        public static void Main(string[] args)
        {
            log.Config();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ALGO DEU ERRADO COM O SERVIÇO XML:" + ex.ToString());
                EventLog eLog = new EventLog("Application");
                eLog.Source = "Application";
                eLog.WriteEntry("OCORREU UMA FALHA : " + ex.ToString(), EventLogEntryType.Error);
                ServiceController sc = new ServiceController();
                sc.Stop();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Service>()
            .Configure<EventLogSettings>(config =>
                {
                    config.LogName = "XmlService";
                    //config.SourceName = ".NET Runtime";
                });
            }).UseWindowsService();
    }
}

