using Serilog;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using VerificadorXml;
using ImportaXml;

namespace ServicoXml
{
    public class Service : BackgroundService
    {
        private static GetXml getXml = new GetXml();
        private static ImportXml import = new ImportXml();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Serviço xml começou a funcionar");

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Debug("getXml (início) ...");

                getXml.ValidateFolder();

                getXml.GetAttatchments();

                Log.Debug("getXml (fim) ...");
                Log.Debug("Import (início) ...");

                import.Import();

                Log.Debug("Import (fim) ...");    
            }

            if (stoppingToken.IsCancellationRequested)
                Log.Warning("O serviço xml foi parado manualmente");
        }
    }
}