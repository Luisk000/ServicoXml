using Serilog;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using VerificadorXml;
using ImportaXml;

namespace ServicoXml
{
    public class Service : BackgroundService
    {
        private static GetXml getXml = new GetXml();
        private static ImportXml import = new ImportXml();
        private static System.Timers.Timer timer = new System.Timers.Timer();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer.Enabled = true;
            timer.Interval = 10000;
            timer.Start();

            Log.Information("Serviço xml funcionando");

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Debug("getXml (início) ...");

                getXml.ValidateFolder();
                getXml.GetAttatchments();

                Log.Debug("getXml (fim) ...");
                Log.Debug("Import (início) ...");

                import.Import();

                Log.Debug("Import (fim) ...");
                throw new Exception();
            }

            if (stoppingToken.IsCancellationRequested)
                Log.Warning("O serviço xml foi parado manualmente");
        }
    }
}