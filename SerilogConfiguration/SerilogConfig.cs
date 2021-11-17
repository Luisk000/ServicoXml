using Microsoft.Extensions.Configuration;
using Serilog;
using SerilogConfiguration.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SerilogConfiguration
{
    public class SerilogConfig
    { 
        private static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("DbSettings.json", optional: false, reloadOnChange: true).Build();
        private static string LogFolder = configuration.GetSection("LogFolder").Value;

        public void Config()
        {
            folderConfig();

            try
            {
                LogLevelDbContext context = new LogLevelDbContext();
                var level = context.Levels.Where(b => b.Id == 1).Single();
                string log = "";

                if (level.Level == 1)
                    log = "Debug.json";

                else if (level.Level == 2)
                    log = "Information.json";

                else if (level.Level == 3)
                    log = "Warning.json";

                else if (level.Level == 4)
                    log = "Error.json";

                else if (level.Level == 5)
                    log = "Fatal.json";

                IConfigurationRoot lConfiguration = new ConfigurationBuilder().AddJsonFile(("Level\\" + log), optional: false, reloadOnChange: true).Build();

                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(lConfiguration)
                .WriteTo.Console()
                .WriteTo.File(LogFolder + "\\log", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            }
            catch (Exception ex)
            {
                try
                {
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(LogFolder + "\\log", rollingInterval: RollingInterval.Hour)
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

        private void folderConfig()
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
        }
    }
}
