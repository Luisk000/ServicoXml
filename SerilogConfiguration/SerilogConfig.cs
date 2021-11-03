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
            if (!Directory.Exists(LogFolder))
                Directory.CreateDirectory(LogFolder);
            try
            {
                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.File(LogFolder + "//log", rollingInterval: RollingInterval.Minute)
                .CreateLogger();
            }
            catch
            {
                try
                {
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(LogFolder + "//log", rollingInterval: RollingInterval.Minute)
                    .CreateLogger();
                }
                catch (Exception ex)
                {
                    EventLog eLog = new EventLog("Application");
                    eLog.Source = "Application";
                    eLog.WriteEntry("SERILOG NÂO ESTÀ FUNCIONANDO : " + ex.ToString(), EventLogEntryType.Error);
                }
            }
        }
    }
}
