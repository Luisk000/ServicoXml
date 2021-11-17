using Limilabs.Client;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public void GetAttatchments()
        {
            ValidateFolder();

            try
            {
                CadastroDbContext context = new CadastroDbContext();
                foreach (Cadastro cadastro in context.Cadastros)
                {
                    if (cadastro.Ativo == true)
                        using (Imap imap = new Imap())
                        {
                            try
                            {
                                imap.Connect(cadastro.ServerImap);
                                imap.UseBestLogin(cadastro.Email, cadastro.Senha);
                                imap.SelectInbox();

                                Serilog.Log.Debug("Conectou-se com sucesso a " + cadastro.Email);

                                List<long> uids = imap.Search(Flag.Undeleted);

                                foreach (long uid in uids)
                                {
                                    var eml = imap.GetMessageByUID(uid);
                                    IMail mail = new MailBuilder().CreateFromEml(eml);

                                    Serilog.Log.Debug("Nova mensagem recebida de " + mail.ReturnPath + " em " + cadastro.Email);

                                    SaveAttachments(mail, folderPendente, cadastro.Email.ToString());

                                    //imap.DeleteMessageByUID(uid);

                                    Serilog.Log.Debug("Finalizou análise de mensagem recebida de " + mail.ReturnPath + " em " + cadastro.Email);
                                }
                                imap.Close();
                            }
                            catch (ServerException ex)
                            {
                                Serilog.Log.Error(ex, "Falha ao conectar-se com " + cadastro.Email);
                            }
                        }
                }
                if (!context.Cadastros.Any())
                    Serilog.Log.Warning("Não há nenhum cadastro no banco de dados");
            }
            catch (SqlException ex)
            {
                Serilog.Log.Fatal(ex, "Falha ao conectar com o servidor SQL: " + ex.ToString());
                throw new Exception();
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
                Serilog.Log.Error(ex, "Um erro desconhecido ocorreu com o verificador: " + ex.ToString());
            }
        }


        private void ValidateFolder()
        {
            try
            {
                if (!Directory.Exists(folderPendente))
                {
                    Serilog.Log.Debug("Diretório 'Pendente' não encontrado, criando nova pasta na Área de Trabalho");
                    Directory.CreateDirectory(folderPendente);
                }

                if (!Directory.Exists(folderAprovado))
                {
                    Serilog.Log.Debug("Diretório 'Aprovado' não encontrado, criando nova pasta na Área de Trabalho");
                    Directory.CreateDirectory(folderAprovado);
                }

                if (!Directory.Exists(folderSemCertificado))
                {
                    Serilog.Log.Debug("Diretório 'Sem Certificado' não encontrado, criando nova pasta na Área de Trabalho");
                    Directory.CreateDirectory(folderSemCertificado);
                }


                if (!Directory.Exists(folderCertificadoInvalido))
                {
                    Serilog.Log.Debug("Diretório 'Certificado Inválido' não encontrado, criando nova pasta na Área de Trabalho");
                    Directory.CreateDirectory(folderCertificadoInvalido);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Serilog.Log.Fatal(ex, "Permissão para modificar diretorios negada: " + ex.ToString());
                throw new Exception();
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Um erro desconhecido ocorreu ao criar os diretórios: " + ex.ToString());
                throw new Exception();
            }
        }


        private void SaveAttachments(IMail email, string folder, string cadastro)
        {
            foreach (MimeData attachment in email.Attachments)
            {
                var file = Path.Combine(folder, attachment.SafeFileName);
                Path.Combine(folder, attachment.SafeFileName);

                if (attachment.ContentType.ToString().Equals("text/xml"))
                {
                    if (!File.Exists(Path.Combine(folderAprovado, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderCertificadoInvalido, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderSemCertificado, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderFalha, attachment.SafeFileName))
                    && !File.Exists(Path.Combine(folderConcluido, attachment.SafeFileName)))
                    {
                        Serilog.Log.Debug("Arquivo xml " + attachment.SafeFileName + " encontrado");
                        attachment.Save(Path.Combine(file));
                        VerifyXML(Path.Combine(file), email, cadastro);
                    }
                    else
                        Serilog.Log.Debug("Há um anexo xml repetido: " + attachment.SafeFileName);
                }
                else
                    Serilog.Log.Debug("Há um anexo que não é um arquivo xml: " + attachment.SafeFileName);             
            }

            if (!email.Attachments.Any())
                Serilog.Log.Debug("Nenhum anexo foi encontrado na mensagem");
        }


        private void VerifyXML(string xmlName, IMail email, string cadastro)
        {
            string sourceFile = Path.Combine(folderPendente, xmlName);
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                Serilog.Log.Debug(xmlName.Split('\\')[6] + " carregado, iniciando verificação...");
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(xmlName);

                SignedXml signedXml = new SignedXml(xmlDoc);
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
                XmlNodeList certificates509 = xmlDoc.GetElementsByTagName("X509Certificate");

                if (certificates509.Count == 0)
                    throw new XmlException();

                X509Certificate2 dcert2 = new X509Certificate2(Convert.FromBase64String(certificates509[0].InnerText));
                foreach (XmlElement element in nodeList)
                {
                    signedXml.LoadXml(element);

                    bool passes = signedXml.CheckSignature(dcert2, true);

                    if (passes)
                    {
                        Serilog.Log.Debug(xmlName.Split('\\')[6] + " é válido");
                        Move(sourceFile, xmlName, folderAprovado, email, cadastro);
                    }
                    else
                    {
                        Serilog.Log.Debug(xmlName.Split('\\')[6] + " não possui um certificado válido");
                        Move(sourceFile, xmlName, folderCertificadoInvalido, email, cadastro);
                    }
                }
            }
            catch (XmlException)
            {
                Serilog.Log.Debug(xmlName.Split('\\')[6] + " não possui um certificado");
                Move(sourceFile, xmlName, folderSemCertificado, email, cadastro);
            }
            catch (CryptographicException)
            {
                Serilog.Log.Debug(xmlName.Split('\\')[6] + " não possui um certificado válido");
                Move(sourceFile, xmlName, folderCertificadoInvalido, email, cadastro);
            }
            catch (FormatException)
            {
                Serilog.Log.Debug(xmlName.Split('\\')[6] + " não possui um certificado válido");
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
                Serilog.Log.Information("(Resumo) Arquivo XML autêntico salvo: " + xmlName.Split('\\')[6]);
                Serilog.Log.Information("Enviado por: " + email.ReturnPath);
                Serilog.Log.Information("Recebido em: " + cadastro);
            }

            if (folder == folderCertificadoInvalido)
            {
                Serilog.Log.Warning("(Resumo) Arquivo XML com certificado inválido salvo: " + xmlName.Split('\\')[6]);
                Serilog.Log.Warning("Enviado por: " + email.ReturnPath);
                Serilog.Log.Warning("Recebido em: " + cadastro);
            }

            if (folder == folderSemCertificado)
            {
                Serilog.Log.Warning("(Resumo) Arquivo XML sem certificado salvo: " + xmlName.Split('\\')[6]);
                Serilog.Log.Warning("Enviado por: " + email.ReturnPath);
                Serilog.Log.Warning("Recebido em: " + cadastro);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
