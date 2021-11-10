using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;

namespace SerilogConfiguration
{
    public class SerilogConfig
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("serilogsettings.json", optional: false, reloadOnChange: true).Build();

        private static string LogFolder = configuration.GetSection("LogFolder").Value;
        public void Config()
        {
            try
            {
                if (!Directory.Exists(LogFolder))
                    Directory.CreateDirectory(LogFolder);
            }
            catch (UnauthorizedAccessException ex)
            {
                EventLog eLog = new EventLog("Application");
                eLog.Source = "Application";
                eLog.WriteEntry("Acesso ao diretorio de armazenamento de Logs negada : " + ex.ToString(), EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                EventLog eLog = new EventLog("Application");
                eLog.Source = "Application";
                eLog.WriteEntry("Um erro desconhecido ocorreu com o Serilog : " + ex.ToString(), EventLogEntryType.Error);
            }

            try
            {
                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File(LogFolder + "//log", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            }
            catch (Exception ex)
            {
                try
                {
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(LogFolder + "//log", rollingInterval: RollingInterval.Hour)
                    .CreateLogger();

                    Log.Error(ex, "Ocorreu um erro ao possivelmente tentar salvar logs no banco de dados: " + ex.ToString());
                }
                catch (Exception x)
                {
                    EventLog eLog = new EventLog("Application");
                    eLog.Source = "Application";
                    eLog.WriteEntry("SERILOG NÂO ESTÁ FUNCIONANDO : " + x.ToString(), EventLogEntryType.Error);
                }
            }
        }
    }
}
