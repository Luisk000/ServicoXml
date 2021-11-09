using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceProcess;
using System.Xml;
using VerificadorXml.Models;

namespace VerificadorXml
{
    public class GetXml
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("registersettings.json", optional: false, reloadOnChange: true).Build();

        protected string folderPendente = configuration.GetSection("FolderLocations:folderPendente").Value;
        protected string folderAprovado = configuration.GetSection("FolderLocations:folderAprovado").Value;
        protected string folderSemCertificado = configuration.GetSection("FolderLocations:folderSemCertificado").Value;
        protected string folderCertificadoInvalido = configuration.GetSection("FolderLocations:folderCertificadoInvalido").Value;
        protected string folderFalha = configuration.GetSection("FolderLocations:folderFalha").Value;
        protected string folderConcluido = configuration.GetSection("FolderLocations:folderConcluido").Value;


        public void ValidateFolder()
        {
            try
            {
                if (!Directory.Exists(folderPendente))
                {
                    Serilog.Log.Warning("Diretório não encontrado, criando nova pasta na Área de Trabalho");
                    Directory.CreateDirectory(folderPendente);
                }
                    
                if (!Directory.Exists(folderAprovado))
                    Directory.CreateDirectory(folderAprovado);

                if (!Directory.Exists(folderSemCertificado))
                    Directory.CreateDirectory(folderSemCertificado);

                if (!Directory.Exists(folderCertificadoInvalido))
                    Directory.CreateDirectory(folderCertificadoInvalido);
            }
            catch (UnauthorizedAccessException ex)
            {
                Serilog.Log.Fatal(ex, "Permissão para modificar diretrios negada: " + ex.ToString());
                ServiceController sc = new ServiceController();
                sc.Stop();
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Um erro desconhecido ocorreu ao criar os diretórios: " + ex.ToString());
                ServiceController sc = new ServiceController();
                sc.Stop();
            }
        }


        public void GetAttatchments()
        {
            try
            {               
                CadastroDbContext context = new CadastroDbContext();                
                foreach (Cadastro cadastro in context.Cadastros)
                {
                    Serilog.Log.Debug("Procurando por anexos em " + cadastro.Email);
                    if (cadastro.Ativo == true)
                        using (Imap imap = new Imap())
                        {
                            imap.Connect(cadastro.ServerImap);
                            imap.UseBestLogin(cadastro.Email, cadastro.Senha);
                            imap.SelectInbox();

                            List<long> uids = imap.Search(Flag.Undeleted);

                            foreach (long uid in uids)
                            {
                                var eml = imap.GetMessageByUID(uid);
                                IMail mail = new MailBuilder().CreateFromEml(eml);

                                SaveAttachments(mail, folderPendente, cadastro.Email.ToString());
                            }
                            imap.Close();
                        }
                }
                if (!context.Cadastros.Any())
                    Serilog.Log.Warning("Não há nehum cadastro no banco de dados");
            }
            catch (SqlException ex)
            {
                Serilog.Log.Fatal(ex, "Falha ao conectar com o servidor SQL: " + ex.ToString());
                ServiceController sc = new ServiceController();
                sc.Stop();
            }
            catch (DirectoryNotFoundException)
            {
                Serilog.Log.Warning("A pasta de destino dos arquivos não foi encontrada, ou foi excluida");
                Serilog.Log.Warning("Criando nova pasta de destino");
            }
            catch (ImapResponseException ex)
            {
                Serilog.Log.Error(ex, "Falha ao conectar com server Imap: " + ex.ToString());
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Um erro desconhecido ocorreu: " + ex.ToString());
            }
        }


        private void SaveAttachments(IMail email, string folder, string cadastro)
        {
            foreach (MimeData attachment in email.Attachments)
            {
                var file = Path.Combine(folder, attachment.SafeFileName);
                Path.Combine(folder, attachment.SafeFileName);

                if (attachment.ContentType.ToString().Equals("text/xml")
                    && !File.Exists(Path.Combine(folderAprovado, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderCertificadoInvalido, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderSemCertificado, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderFalha, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderConcluido, attachment.SafeFileName)))
                {                    
                    attachment.Save(Path.Combine(file));
                    VerifyXML(Path.Combine(file), email, cadastro);
                }
            }

            if (!email.Attachments.Any())
                Serilog.Log.Warning("Nenhum anexo foi encontrado em um email enviado por " + email.ReturnPath);
        }


        private void VerifyXML(string xmlName, IMail email, string cadastro)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlName);

            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            XmlNodeList certificates509 = xmlDoc.GetElementsByTagName("X509Certificate");

            string sourceFile = Path.Combine(folderPendente, xmlName);

            if (!sourceFile.Any())
                Serilog.Log.Information("Nenhum novo arquivo xml recebido de " + email.ReturnPath);

            if (certificates509.Count == 0)
            {
                Move(sourceFile, xmlName, folderSemCertificado, email, cadastro);
                return;
            }

            try
            {
                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates509[0].InnerText));
                foreach (XmlElement element in nodeList)
                {
                    signedXml.LoadXml(element);

                    bool passes = signedXml.CheckSignature(dcert2, true);

                    if (passes)
                        Move(sourceFile, xmlName, folderAprovado, email, cadastro);

                    else
                        Move(sourceFile, xmlName, folderCertificadoInvalido, email, cadastro);
                }
            }
            catch
            {
                Move(sourceFile, xmlName, folderCertificadoInvalido, email, cadastro);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        private void Move(string sourceFile, string xmlName, string folder, IMail email, string cadastro)
        {
            string destinationFile = Path.Combine(folder, Path.GetFileName(xmlName));

            if (!File.Exists(destinationFile))
                File.Move(sourceFile, destinationFile);

            if (folder == folderAprovado)
            {
                Serilog.Log.Information("Arquivo XML autêntico recebido");
                Serilog.Log.Information("Enviado por: " + email.ReturnPath);
                Serilog.Log.Information("Recebido em: " + cadastro);
                Serilog.Log.Information("Nome do Arquivo: " + xmlName.Split('\\')[6]);
            }
                
            if (folder == folderCertificadoInvalido)
            {
                Serilog.Log.Warning("Arquivo XML com certificado inválido recebido");
                Serilog.Log.Warning("Enviado por: " + email.ReturnPath);
                Serilog.Log.Warning("Recebido em: " + cadastro);
                Serilog.Log.Warning("Nome do Arquivo: " + xmlName.Split('\\')[6]);
            }             

            if (folder == folderSemCertificado)
            {
                Serilog.Log.Warning("Arquivo XML sem certificado recebido");
                Serilog.Log.Warning("Enviado por: " + email.ReturnPath);
                Serilog.Log.Warning("Recebido em: " + cadastro);
                Serilog.Log.Warning("Nome do Arquivo: " + xmlName.Split('\\')[6]);
            }
                
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
