using Serilog;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using VerificadorXml;
using ImportaXml;
using System.ServiceProcess;

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
                try
                {
                    Log.Debug("getXml (início) ...");

                    getXml.ValidateFolder();

                    getXml.GetAttatchments();

                    Log.Debug("getXml (fim) ...");
                    Log.Debug("Import (início) ...");

                    import.Import();

                    Log.Debug("Import (fim) ...");
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    Log.Fatal(ex, "Ocorreu um erro com o serviço xml: " + ex.ToString());
                    ServiceController sc = new ServiceController();
                    sc.Stop();
                }                
            }

            if (stoppingToken.IsCancellationRequested)
                Log.Warning("O serviço xml foi parado manualmente");
        }
    }
}