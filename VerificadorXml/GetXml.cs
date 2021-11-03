using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Microsoft.Data.SqlClient;
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
    public class GetXml : DataRepository
    {
        public void ValidateFolder()
        {
            if (!Directory.Exists(folderPendente))
                Directory.CreateDirectory(folderPendente);

            if (!Directory.Exists(folderAprovado))
                Directory.CreateDirectory(folderAprovado);

            if (!Directory.Exists(folderSemCertificado))
                Directory.CreateDirectory(folderSemCertificado);

            if (!Directory.Exists(folderCertificadoInvalido))
                Directory.CreateDirectory(folderCertificadoInvalido);
        }

        public void GetAttatchments()
        {
            try
            {               
                CadastroDbContext context = new CadastroDbContext();
                foreach (Cadastro cadastro in context.Cadastros)
                {

                    if (cadastro.Ativo == true)
                        using (Imap imap = new Imap())
                        {
                            imap.Connect(cadastro.ServerImap);
                            imap.UseBestLogin(cadastro.Email, cadastro.Senha);
                            imap.SelectInbox();

                            List<long> uids = imap.Search(Flag.All);

                            foreach (long uid in uids)
                            {
                                var eml = imap.GetMessageByUID(uid);
                                IMail mail = new MailBuilder().CreateFromEml(eml);

                                SaveAttachments(mail, folderPendente);
                            }
                            imap.Close();
                        }
                }
            }
            catch (SqlException ex)
            {
                Serilog.Log.Fatal(ex, "Falha ao conectar com o servidor SQL: " + ex.ToString());
                ServiceController sc = new ServiceController();
                sc.Stop();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Falha ao conectar com server Imap: " + ex.ToString());
            }
        }

        private void SaveAttachments(IMail email, string folder)
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
                    VerifyXML(Path.Combine(file));
                }
            }
        }

        private void VerifyXML(string xmlName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlName);

            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
            XmlNodeList certificates509 = xmlDoc.GetElementsByTagName("X509Certificate");

            string sourceFile = Path.Combine(folderPendente, xmlName);

            if (!sourceFile.Any())
                Serilog.Log.Information("Nenhum novo arquivo xml recebido");

            if (certificates509.Count == 0)
            {
                Move(sourceFile, xmlName, folderSemCertificado);
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
                        Move(sourceFile, xmlName, folderAprovado);

                    else
                        Move(sourceFile, xmlName, folderCertificadoInvalido);
                }
            }
            catch
            {
                Move(sourceFile, xmlName, folderCertificadoInvalido);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void Move(string sourceFile, string xmlName, string folder)
        {
            string destinationFile = Path.Combine(folder, Path.GetFileName(xmlName));

            if (!File.Exists(destinationFile))
                File.Move(sourceFile, destinationFile);

            if (folder == folderAprovado)
                Serilog.Log.Information("Arquivo XML autêntico recebido");

            if (folder == folderCertificadoInvalido)
                Serilog.Log.Warning("Arquivo XML com certificado inválido recebido");

            if (folder == folderSemCertificado)
                Serilog.Log.Warning("Arquivo XML sem certificado recebido");

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
