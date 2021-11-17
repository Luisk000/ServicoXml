using Serilog;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using VerificadorXml;
using ImportaXml;
using System.Management;
using System.Security.Principal;

namespace ServicoXml
{
    public class Service : BackgroundService
    {
        private static GetXml getXml = new GetXml();
        private static ImportXml import = new ImportXml();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetUser();

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Debug("Verificando email (início) ...");
                getXml.GetAttatchments();
                Log.Debug("Verificando email (fim) ...");

                Log.Debug("Importando dados (início) ...");
                import.Import();
                Log.Debug("Importando dados (fim) ...");
            }

            if (stoppingToken.IsCancellationRequested)
                Log.Warning("O serviço xml foi finalizado manualmente por " + WindowsIdentity.GetCurrent().Name);
        }

        private static void GetUser()
        {
            Log.Information("Serviço Xml foi iniciado por " + WindowsIdentity.GetCurrent().Name);

            SelectQuery sQuery = new SelectQuery(string.Format("select name, startname from Win32_Service"));
            using (ManagementObjectSearcher mgmtSearcher = new ManagementObjectSearcher(sQuery))
            {
                foreach (ManagementObject service in mgmtSearcher.Get())
                {
                    if (service["Name"].ToString() == "XmlService")
                        Log.Information("O serviço conectou-se utilizando " + service["startname"].ToString());
                }
            }
        }
    }
}