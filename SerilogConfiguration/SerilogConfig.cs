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
                Log.Error(ex, "Permissão para modificar diretorio de Logs negada: " + ex.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Um erro desconhecido ocorreu ao criar o diretório para Logs: " + ex.ToString());
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
