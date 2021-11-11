using ImportaXml.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Xml;

namespace ImportaXml
{
    public class ImportXml
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("filesettings.json", optional: false, reloadOnChange: true).Build();

        private static string folderAprovado = configuration.GetSection("folder").Value;
        private static string folderConcluido = configuration.GetSection("folderConcluido").Value;
        private static string folderFalha = configuration.GetSection("folderFalha").Value;

        private readonly XmlDbContext context = new XmlDbContext();


        public void Import()
        {
            if (!Directory.Exists(folderAprovado))
            {
                Log.Error("A pasta de armazenamento de Xml não foi encontrada!");
                return;
            }

            string[] arquivos = Directory.GetFiles(folderAprovado);

            if (arquivos.Any())
            {
                foreach (string arq in arquivos)
                {
                    XmlFile xml = new XmlFile();
                    xml.XmlName = arq.Split('\\')[6];

                    try
                    {                                            
                        ReadXml(arq, xml);

                        Log.Information("Leitura de " + xml.XmlName + " concluída com sucesso");

                        context.Files.Add(xml);
                        context.SaveChanges();

                        Log.Information(xml.XmlName + " adicionado ao banco de dados");
                        Move(arq, folderConcluido);
                    }
                    catch (SqlException ex)
                    {
                        Log.Fatal(ex, "Falha ao conectar com o servidor SQL: " + ex.ToString());
                        Move(arq, folderFalha);
                        ServiceController sc = new ServiceController();
                        sc.Stop();
                    }
                    catch (DbUpdateException)
                    {
                        try
                        {
                            var nomeRepetido = context.Files.Where(b => b.XmlName == xml.XmlName).Single();      
                            Log.Warning(xml.XmlName + " já estava salvo no banco de dados");
                            Move(arq, folderFalha);
                        }
                        catch
                        {
                            var idRepetido = context.Files.Where(b => b.nfeProc_NFe_infNFe_____Id == xml.nfeProc_NFe_infNFe_____Id).Single();
                            Log.Warning("Há uma arquivo com o mesmo Id de " + xml.XmlName + " já salvo no banco de dados: " + idRepetido.XmlName);
                            Move(arq, folderFalha);
                        }                
                    }
                    catch (InvalidOperationException)
                    {
                        try
                        {
                            var nomeRepetido = context.Files.Where(b => b.XmlName == xml.XmlName).Single();
                            Log.Warning(xml.XmlName + " já estava salvo no banco de dados");
                            Move(arq, folderFalha);
                        }
                        catch
                        {
                            var idRepetido = context.Files.Where(b => b.nfeProc_NFe_infNFe_____Id == xml.nfeProc_NFe_infNFe_____Id).Single();
                            Log.Warning("Há uma arquivo com o mesmo Id de " + xml.XmlName + " já salvo no banco de dados: " + idRepetido.XmlName);
                            Move(arq, folderFalha);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Um erro desconhecido ocorreu ao ler um arquivo xml: " + ex.ToString());
                    }
                }
            }
            else
            {
                Log.Debug("Nenhum novo arquivo xml recebido no diretório local");
            }
        }


        private void ReadXml(string arq, XmlFile xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(arq);

            XmlNodeList node = xmlDoc.GetElementsByTagName("nfeProc");
            XmlAttributeCollection nfeProcAt = node[0].Attributes;

            for (int f = 0; f < nfeProcAt.Count; f++)
            {
                if (nfeProcAt[f].Name == "versao")
                    xml.nfeProc____versao = nfeProcAt[f].InnerText;

                else if (nfeProcAt[f].Name == "xmlns")
                    xml.nfeProc____xmlns = nfeProcAt[f].InnerText;
            }

            XmlNodeList nfeProc = node[0].ChildNodes;

            for (int f = 0; f < nfeProc.Count; f++)
            {
                if (nfeProc[f].Name == "NFe")
                {
                    XmlAttributeCollection NFeAt = nfeProc[f].Attributes;

                    for (int g = 0; g < NFeAt.Count; g++)
                    {
                        if (nfeProcAt[g].Name == "xmlns")
                            xml.nfeProc_Nfe____xmlns = NFeAt[g].InnerText;
                    }

                    XmlNodeList NFe = nfeProc[f].ChildNodes;

                    for (int g = 0; g < NFe.Count; g++)
                    {
                        if (NFe[g].Name == "infNFe")
                        {
                            XmlAttributeCollection infNFeAt = NFe[g].Attributes;

                            for (int h = 0; h < infNFeAt.Count; h++)
                            {
                                if (infNFeAt[h].Name == "Id")
                                {
                                    xml.nfeProc_NFe_infNFe_____Id = infNFeAt[h].InnerText;
                                }

                                else if (infNFeAt[h].Name == "versao")
                                    xml.nfeProc_Nfe_infNFe____versao = infNFeAt[h].InnerText;
                            }

                            XmlNodeList infNFe = NFe[g].ChildNodes;

                            for (int h = 0; h < infNFe.Count; h++)
                            {
                                if (infNFe[h].Name == "ide")
                                {
                                    XmlNodeList ide = infNFe[h].ChildNodes;

                                    for (int i = 0; i < ide.Count; i++)
                                    {
                                        if (ide[i].Name == "cUF")
                                            xml.nfeProc_NFe_infNFe_ide_cUF = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "cNF")
                                            xml.nfeProc_NFe_infNFe_ide_cNF = ide[i].InnerText;

                                        else if (ide[i].Name == "natOp")
                                            xml.nfeProc_NFe_infNFe_ide_natOp = ide[i].InnerText;

                                        else if (ide[i].Name == "mod")
                                            xml.nfeProc_NFe_infNFe_ide_mod = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "serie")
                                            xml.nfeProc_NFe_infNFe_ide_serie = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "nNF")
                                            xml.nfeProc_NFe_infNFe_ide_nNF = double.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "dhEmi")
                                            xml.nfeProc_NFe_infNFe_ide_dhEmi = ide[i].InnerText;

                                        else if (ide[i].Name == "dhSaiEnt")
                                            xml.nfeProc_NFe_infNFe_ide_dhSaiEnt = ide[i].InnerText;

                                        else if (ide[i].Name == "tpNF")
                                            xml.nfeProc_NFe_infNFe_ide_tpNF = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "idDest")
                                            xml.nfeProc_NFe_infNFe_ide_idDest = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "cMunFG")
                                            xml.nfeProc_NFe_infNFe_ide_cMunFG = ide[i].InnerText;

                                        else if (ide[i].Name == "tpImp")
                                            xml.nfeProc_NFe_infNFe_ide_tpImp = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "tpEmis")
                                            xml.nfeProc_NFe_infNFe_ide_tpEmis = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "cDV")
                                            xml.nfeProc_NFe_infNFe_ide_cDV = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "tpAmb")
                                            xml.nfeProc_NFe_infNFe_ide_tpAmb = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "finNFe")
                                            xml.nfeProc_NFe_infNFe_ide_finNFe = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "indFinal")
                                            xml.nfeProc_NFe_infNFe_ide_indFinal = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "indPres")
                                            xml.nfeProc_NFe_infNFe_ide_indPres = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "indIntermed")
                                            xml.nfeProc_NFe_infNFe_ide_indIntermed = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "procEmi")
                                            xml.nfeProc_NFe_infNFe_ide_procEmi = Int32.Parse(ide[i].InnerText);

                                        else if (ide[i].Name == "verProc")
                                            xml.nfeProc_NFe_infNFe_ide_verProc = ide[i].InnerText;

                                        else
                                            NaoRegistrado(ide[i].Name, ide[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "emit")
                                {
                                    XmlNodeList emit = infNFe[h].ChildNodes;

                                    for (int i = 0; i < emit.Count; i++)
                                    {
                                        if (emit[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_emit_CNPJ = emit[i].InnerText;

                                        else if (emit[i].Name == "CPF")
                                            xml.nfeProc_NFe_infNFe_emit_CPF = emit[i].InnerText;

                                        else if (emit[i].Name == "xNome")
                                            xml.nfeProc_NFe_infNFe_emit_xNome = emit[i].InnerText;

                                        else if (emit[i].Name == "xFant")
                                            xml.nfeProc_NFe_infNFe_emit_xFant = emit[i].InnerText;

                                        else if (emit[i].Name == "enderEmit")
                                        {
                                            XmlNodeList enderEmit = emit[i].ChildNodes;
                                            for (int j = 0; j < enderEmit.Count; j++)
                                            {
                                                if (enderEmit[j].Name == "xLgr")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_xLgr = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "nro")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_nro = Int32.Parse(enderEmit[j].InnerText);

                                                else if (enderEmit[j].Name == "xCpl")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_xCpl = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "xBairro")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_xBairro = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "cMun")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_cMun = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "xMun")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_xMun = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "UF")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_UF = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "CEP")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_CEP = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "cPais")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_cPais = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "xPais")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_xPais = enderEmit[j].InnerText;

                                                else if (enderEmit[j].Name == "fone")
                                                    xml.nfeProc_NFe_infNFe_emit_enderEmit_fone = enderEmit[j].InnerText;

                                                else
                                                    NaoRegistrado(enderEmit[j].Name, enderEmit[j].InnerText, xml);
                                            }

                                        }

                                        else if (emit[i].Name == "IE")
                                            xml.nfeProc_NFe_infNFe_emit_IE = emit[i].InnerText;

                                        else if (emit[i].Name == "IEST")
                                            xml.nfeProc_NFe_infNFe_emit_IEST = emit[i].InnerText;

                                        else if (emit[i].Name == "IM")
                                            xml.nfeProc_NFe_infNFe_emit_IM = emit[i].InnerText;

                                        else if (emit[i].Name == "CNAE")
                                            xml.nfeProc_NFe_infNFe_emit_CNAE = emit[i].InnerText;

                                        else if (emit[i].Name == "CRT")
                                            xml.nfeProc_NFe_infNFe_emit_CRT = Int32.Parse(emit[i].InnerText);

                                        else if (emit[i].Name == "indIEDest")
                                            xml.nfeProc_NFe_infNFe_emit_indIEDest = emit[i].InnerText;

                                        else if (emit[i].Name == "email")
                                            xml.nfeProc_NFe_infNFe_emit_email = emit[i].InnerText;

                                        else
                                            NaoRegistrado(emit[i].Name, emit[i].InnerText, xml);
                                    }

                                }

                                else if (infNFe[h].Name == "dest")
                                {
                                    XmlNodeList dest = infNFe[h].ChildNodes;

                                    for (int i = 0; i < dest.Count; i++)
                                    {
                                        if (dest[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_dest_CNPJ = dest[i].InnerText;

                                        else if (dest[i].Name == "CPF")
                                            xml.nfeProc_NFe_infNFe_dest_CPF = dest[i].InnerText;

                                        else if (dest[i].Name == "idEstrangeiro")
                                            xml.nfeProc_NFe_infNFe_dest_idEstrangeiro = dest[i].InnerText;

                                        else if (dest[i].Name == "xNome")
                                            xml.nfeProc_NFe_infNFe_dest_xNome = dest[i].InnerText;

                                        else if (dest[i].Name == "enderDest")
                                        {
                                            XmlNodeList enderDest = dest[i].ChildNodes;
                                            for (int j = 0; j < enderDest.Count; j++)
                                            {
                                                if (enderDest[j].Name == "xLgr")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_xLgr = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "nro")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_xCpl = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "xCpl")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_nro = Int32.Parse(enderDest[j].InnerText);

                                                else if (enderDest[j].Name == "xBairro")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_xBairro = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "cMun")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_cMun = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "xMun")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_xMun = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "UF")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_UF = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "CEP")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_CEP = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "cPais")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_cPais = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "xPais")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_xPais = enderDest[j].InnerText;

                                                else if (enderDest[j].Name == "fone")
                                                    xml.nfeProc_NFe_infNFe_dest_enderDest_fone = enderDest[j].InnerText;

                                                else
                                                    NaoRegistrado(enderDest[j].Name, enderDest[j].InnerText, xml);
                                            }
                                        }

                                        else if (dest[i].Name == "indIEDest")
                                            xml.nfeProc_NFe_infNFe_dest_indIEDest = Int32.Parse(dest[i].InnerText);

                                        else if (dest[i].Name == "IE")
                                            xml.nfeProc_NFe_infNFe_dest_IE = dest[i].InnerText;

                                        else if (dest[i].Name == "ISUF")
                                            xml.nfeProc_NFe_infNFe_dest_ISUF = dest[i].InnerText;

                                        else if (dest[i].Name == "IM")
                                            xml.nfeProc_NFe_infNFe_dest_IM = dest[i].InnerText;

                                        else if (dest[i].Name == "email")
                                            xml.nfeProc_NFe_infNFe_dest_email = dest[i].InnerText;

                                        else
                                            NaoRegistrado(dest[i].Name, dest[i].InnerText, xml);
                                    }

                                }

                                else if (infNFe[h].Name == "retirada")
                                {
                                    XmlNodeList retirada = infNFe[h].ChildNodes;

                                    for (int i = 0; i < retirada.Count; i++)
                                    {
                                        if (retirada[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_retirada_CNPJ = retirada[i].InnerText;

                                        else if (retirada[i].Name == "xNome")
                                            xml.nfeProc_NFe_infNFe_retirada_xNome = retirada[i].InnerText;

                                        else if (retirada[i].Name == "xLgr")
                                            xml.nfeProc_NFe_infNFe_retirada_xLgr = retirada[i].InnerText;

                                        else if (retirada[i].Name == "nro")
                                            xml.nfeProc_NFe_infNFe_retirada_nro = Int32.Parse(retirada[i].InnerText);

                                        else if (retirada[i].Name == "xBairro")
                                            xml.nfeProc_NFe_infNFe_retirada_xBairro = retirada[i].InnerText;

                                        else if (retirada[i].Name == "cMun")
                                            xml.nfeProc_NFe_infNFe_retirada_cMun = retirada[i].InnerText;

                                        else if (retirada[i].Name == "xMun")
                                            xml.nfeProc_NFe_infNFe_retirada_xMun = retirada[i].InnerText;

                                        else if (retirada[i].Name == "UF")
                                            xml.nfeProc_NFe_infNFe_retirada_UF = retirada[i].InnerText;

                                        else if (retirada[i].Name == "CEP")
                                            xml.nfeProc_NFe_infNFe_retirada_CEP = retirada[i].InnerText;

                                        else if (retirada[i].Name == "cPais")
                                            xml.nfeProc_NFe_infNFe_retirada_cPais = retirada[i].InnerText;

                                        else if (retirada[i].Name == "xPais")
                                            xml.nfeProc_NFe_infNFe_retirada_xPais = retirada[i].InnerText;

                                        else if (retirada[i].Name == "fone")
                                            xml.nfeProc_NFe_infNFe_retirada_fone = retirada[i].InnerText;

                                        else if (retirada[i].Name == "email")
                                            xml.nfeProc_NFe_infNFe_retirada_email = retirada[i].InnerText;

                                        else if (retirada[i].Name == "IE")
                                            xml.nfeProc_NFe_infNFe_retirada_IE = retirada[i].InnerText;

                                        else
                                            NaoRegistrado(retirada[i].Name, retirada[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "entrega")
                                {
                                    XmlNodeList entrega = infNFe[h].ChildNodes;

                                    for (int i = 0; i < entrega.Count; i++)
                                    {
                                        if (entrega[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_entrega_CNPJ = entrega[i].InnerText;

                                        else if (entrega[i].Name == "xNome")
                                            xml.nfeProc_NFe_infNFe_entrega_xNome = entrega[i].InnerText;

                                        else if (entrega[i].Name == "xLgr")
                                            xml.nfeProc_NFe_infNFe_entrega_xLgr = entrega[i].InnerText;

                                        else if (entrega[i].Name == "nro")
                                            xml.nfeProc_NFe_infNFe_entrega_nro = Int32.Parse(entrega[i].InnerText);

                                        else if (entrega[i].Name == "xBairro")
                                            xml.nfeProc_NFe_infNFe_entrega_xBairro = entrega[i].InnerText;

                                        else if (entrega[i].Name == "cMun")
                                            xml.nfeProc_NFe_infNFe_entrega_cMun = entrega[i].InnerText;

                                        else if (entrega[i].Name == "xMun")
                                            xml.nfeProc_NFe_infNFe_entrega_xMun = entrega[i].InnerText;

                                        else if (entrega[i].Name == "UF")
                                            xml.nfeProc_NFe_infNFe_entrega_UF = entrega[i].InnerText;

                                        else if (entrega[i].Name == "CEP")
                                            xml.nfeProc_NFe_infNFe_entrega_CEP = entrega[i].InnerText;

                                        else if (entrega[i].Name == "cPais")
                                            xml.nfeProc_NFe_infNFe_entrega_cPais = entrega[i].InnerText;

                                        else if (entrega[i].Name == "xPais")
                                            xml.nfeProc_NFe_infNFe_entrega_xPais = entrega[i].InnerText;

                                        else if (entrega[i].Name == "fone")
                                            xml.nfeProc_NFe_infNFe_entrega_fone = entrega[i].InnerText;

                                        else if (entrega[i].Name == "email")
                                            xml.nfeProc_NFe_infNFe_entrega_email = entrega[i].InnerText;

                                        else if (entrega[i].Name == "IE")
                                            xml.nfeProc_NFe_infNFe_entrega_IE = entrega[i].InnerText;

                                        else
                                            NaoRegistrado(entrega[i].Name, entrega[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "autXML")
                                {
                                    XmlNodeList autXml = infNFe[h].ChildNodes;

                                    for (int i = 0; i < autXml.Count; i++)
                                    {
                                        if (autXml[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_autXml_CNPJ = autXml[i].InnerText;

                                        else if (autXml[i].Name == "CPF")
                                            xml.nfeProc_NFe_infNFe_autXml_CPF = autXml[i].InnerText;

                                        else
                                            NaoRegistrado(autXml[i].Name, autXml[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "det")
                                {
                                    XmlFileDet xmlDet = new XmlFileDet();

                                    XmlAttributeCollection detAt = infNFe[h].Attributes;

                                    for (int i = 0; i < detAt.Count; i++)
                                    {
                                        if (detAt[i].Name == "nItem")
                                            xmlDet.nfeProc_NFe_infNFe_det____nItem = Int32.Parse(detAt[i].InnerText);
                                    }

                                    XmlNodeList det = infNFe[h].ChildNodes;

                                    for (int i = 0; i < det.Count; i++)
                                    {
                                        if (det[i].Name == "prod")
                                        {
                                            XmlNodeList prod = det[i].ChildNodes;

                                            for (int j = 0; j < prod.Count; j++)
                                            {
                                                if (prod[j].Name == "cProd")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_cProd = Int32.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "cEAN")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_cEAN = prod[j].InnerText;

                                                else if (prod[j].Name == "xProd")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_xProd = prod[j].InnerText;

                                                else if (prod[j].Name == "NCM")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_NCM = prod[j].InnerText;

                                                else if (prod[j].Name == "CFOP")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_CFOP = Int32.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "uCom")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_uCom = prod[j].InnerText;

                                                else if (prod[j].Name == "qCom")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_qCom = double.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "vUnCom")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_vUnCom = double.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "vProd")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_vProd = double.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "cEANTrib")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_cEANTrib = prod[j].InnerText;

                                                else if (prod[j].Name == "uTrib")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_uTrib = prod[j].InnerText;

                                                else if (prod[j].Name == "qTrib")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_qTrib = double.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "vUnTrib")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_vUnTrib = double.Parse(prod[j].InnerText);

                                                else if (prod[j].Name == "indTot")
                                                    xmlDet.nfeProc_NFe_infNFe_det_prod_indTot = Int32.Parse(prod[j].InnerText);

                                                else
                                                    NaoRegistrado(prod[j].Name, prod[j].InnerText, xml);
                                            }
                                        }

                                        else if (det[i].Name == "imposto")
                                        {
                                            XmlNodeList imposto = det[i].ChildNodes;

                                            for (int j = 0; j < imposto.Count; j++)
                                            {
                                                if (imposto[j].Name == "ICMS")
                                                {
                                                    XmlNodeList ICMS = imposto[j].ChildNodes;

                                                    for (int k = 0; k < ICMS.Count; k++)
                                                    {
                                                        if (ICMS[k].Name == "ICMS00" || ICMS[k].Name == "ICMSSN102")
                                                        {
                                                            XmlNodeList ICMS00 = ICMS[k].ChildNodes;

                                                            for (int l = 0; l < ICMS00.Count; l++)
                                                            {
                                                                if (ICMS00[l].Name == "orig" && ICMS[k].Name == "ICMS00")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_orig = Int32.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "CST")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_CST = Int32.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "modBC")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_modBC = Int32.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "vBC")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vBC = double.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "pICMS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_pICMS = double.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "vICMS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vICMS = double.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "orig" && ICMS[k].Name == "ICMSSN102")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_orig = Int32.Parse(ICMS00[l].InnerText);

                                                                else if (ICMS00[l].Name == "CSOSN")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_CSOSN = double.Parse(ICMS00[l].InnerText);

                                                                else
                                                                    NaoRegistrado(ICMS00[l].Name, ICMS00[l].InnerText, xml);
                                                            }
                                                        }

                                                        else
                                                            NaoRegistrado(ICMS[k].Name, ICMS[k].InnerText, xml);
                                                    }
                                                }

                                                else if (imposto[j].Name == "IPI")
                                                {
                                                    XmlNodeList IPI = imposto[j].ChildNodes;

                                                    for (int k = 0; k < IPI.Count; k++)
                                                    {
                                                        if (IPI[k].Name == "cEnq")
                                                            xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_cEnq = double.Parse(IPI[k].InnerText);

                                                        else if (IPI[k].Name == "IPITrib" || IPI[k].Name == "IPINT")
                                                        {
                                                            XmlNodeList IPITrib = IPI[k].ChildNodes;

                                                            for (int l = 0; l < IPITrib.Count; l++)
                                                            {
                                                                if (IPITrib[l].Name == "CST" && IPI[k].Name == "IPITrib")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_CST = Int32.Parse(IPITrib[l].InnerText);

                                                                else if (IPITrib[l].Name == "CST" && IPI[k].Name == "IPINT")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_IPINT_CST = Int32.Parse(IPITrib[l].InnerText);

                                                                else if (IPITrib[l].Name == "vBC")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vBC = double.Parse(IPITrib[l].InnerText);

                                                                else if (IPITrib[l].Name == "pIPI")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_pIPI = double.Parse(IPITrib[l].InnerText);

                                                                else if (IPITrib[l].Name == "vIPI")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vIPI = double.Parse(IPITrib[l].InnerText);

                                                                else
                                                                    NaoRegistrado(IPITrib[l].Name, IPITrib[l].InnerText, xml);
                                                            }
                                                        }

                                                        else
                                                            NaoRegistrado(IPI[k].Name, IPI[k].InnerText, xml);
                                                    }
                                                }

                                                else if (imposto[j].Name == "PIS")
                                                {
                                                    XmlNodeList PIS = imposto[j].ChildNodes;

                                                    for (int k = 0; k < PIS.Count; k++)
                                                    {
                                                        if (PIS[k].Name == "PISAliq")
                                                        {
                                                            XmlNodeList PISAliq = PIS[k].ChildNodes;

                                                            for (int l = 0; l < PISAliq.Count; l++)
                                                            {
                                                                if (PISAliq[l].Name == "CST")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_CST = Int32.Parse(PISAliq[l].InnerText);

                                                                else if (PISAliq[l].Name == "vBC")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vBC = double.Parse(PISAliq[l].InnerText);

                                                                else if (PISAliq[l].Name == "pPIS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_pPIS = double.Parse(PISAliq[l].InnerText);

                                                                else if (PISAliq[l].Name == "vPIS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vPIS = double.Parse(PISAliq[l].InnerText);

                                                                else
                                                                    NaoRegistrado(PISAliq[l].Name, PISAliq[l].InnerText, xml);
                                                            }
                                                        }
                                                        else if (PIS[k].Name == "PISNT")
                                                        {
                                                            XmlNodeList PISNT = PIS[k].ChildNodes;

                                                            for (int l = 0; l < PISNT.Count; l++)
                                                            {
                                                                if (PISNT[l].Name == "CST")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_PIS_PISNT_CST = Int32.Parse(PISNT[l].InnerText);

                                                                else
                                                                    NaoRegistrado(PISNT[l].Name, PISNT[l].InnerText, xml);
                                                            }
                                                        }
                                                        else
                                                            NaoRegistrado(PIS[k].Name, PIS[k].InnerText, xml);

                                                    }
                                                }

                                                else if (imposto[j].Name == "COFINS")
                                                {
                                                    XmlNodeList COFINS = imposto[j].ChildNodes;

                                                    for (int k = 0; k < COFINS.Count; k++)
                                                    {
                                                        if (COFINS[k].Name == "COFINSAliq")
                                                        {
                                                            XmlNodeList COFINSAliq = COFINS[k].ChildNodes;

                                                            for (int l = 0; l < COFINSAliq.Count; l++)
                                                            {
                                                                if (COFINSAliq[l].Name == "CST")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_CST = Int32.Parse(COFINSAliq[l].InnerText);

                                                                else if (COFINSAliq[l].Name == "vBC")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vBC = double.Parse(COFINSAliq[l].InnerText);

                                                                else if (COFINSAliq[l].Name == "pCOFINS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pCOFINS = double.Parse(COFINSAliq[l].InnerText);

                                                                else if (COFINSAliq[l].Name == "vCOFINS")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vCOFINS = double.Parse(COFINSAliq[l].InnerText);

                                                                else
                                                                    NaoRegistrado(COFINSAliq[l].Name, COFINSAliq[l].InnerText, xml);
                                                            }
                                                        }
                                                        else if (COFINS[k].Name == "COFINSNT")
                                                        {
                                                            XmlNodeList COFINSNT = COFINS[k].ChildNodes;

                                                            for (int l = 0; l < COFINSNT.Count; l++)
                                                            {
                                                                if (COFINSNT[l].Name == "CST")
                                                                    xmlDet.nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSNT_CST = Int32.Parse(COFINSNT[l].InnerText);

                                                                else
                                                                    NaoRegistrado(COFINSNT[l].Name, COFINSNT[l].InnerText, xml);
                                                            }                                                                                                
                                                        }     

                                                        else
                                                            NaoRegistrado(COFINS[k].Name, COFINS[k].InnerText, xml);
                                                    }
                                                }

                                                else
                                                    NaoRegistrado(imposto[j].Name, imposto[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(det[i].Name, det[i].InnerText, xml);
                                    }

                                    xmlDet.XmlFileId = xml.nfeProc_NFe_infNFe_____Id;
                                    context.Dets.Add(xmlDet);
                                }

                                else if (infNFe[h].Name == "total")
                                {
                                    XmlNodeList total = infNFe[h].ChildNodes;

                                    for (int i = 0; i < total.Count; i++)
                                    {
                                        if (total[i].Name == "ICMSTot")
                                        {
                                            XmlNodeList ICMSTot = total[i].ChildNodes;

                                            for (int j = 0; j < ICMSTot.Count; j++)
                                            {
                                                if (ICMSTot[j].Name == "vBC")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vBC = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vICMS")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vICMS = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vICMSDeson")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vICMSDeson = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vFCPUFDest")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vFCPUFDest = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vICMSUFDest")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFDest = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vICMSUFRemet")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFRemet = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vFCP")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vFCP = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vBCST")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vBCST = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vST")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vST = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vFCPST")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vFCPST = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vFCPSTRet")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vFCPSTRet = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vProd")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vProd = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vFrete")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vFrete = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vSeg")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vSeg = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vDesc")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vDesc = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vII")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vII = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vIPI")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vIPI = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vIPIDevol")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vIPIDevol = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vPIS")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vPIS = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vCOFINS")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vCOFINS = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vOutro")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vOutro = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vNF")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vNF = double.Parse(ICMSTot[j].InnerText);

                                                else if (ICMSTot[j].Name == "vTotTrib")
                                                    xml.nfeProc_NFe_infNFe_total_ICMSTot_vTotTrib = double.Parse(ICMSTot[j].InnerText);

                                                else
                                                    NaoRegistrado(ICMSTot[j].Name, ICMSTot[j].InnerText, xml);
                                            }
                                        }
                                    }
                                }

                                else if (infNFe[h].Name == "transp")
                                {
                                    XmlNodeList transp = infNFe[h].ChildNodes;

                                    for (int i = 0; i < transp.Count; i++)
                                    {
                                        if (transp[i].Name == "modFrete")
                                            xml.nfeProc_NFe_infNFe_total_transp_modFrete = Int32.Parse(transp[i].InnerText);

                                        else if (transp[i].Name == "transporta")
                                        {
                                            XmlNodeList transporta = transp[i].ChildNodes;

                                            for (int j = 0; j < transporta.Count; j++)
                                            {
                                                if (transporta[j].Name == "CNPJ")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_CNPJ = transporta[j].InnerText;

                                                else if (transporta[j].Name == "xNome")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_xNome = transporta[j].InnerText;

                                                else if (transporta[j].Name == "IE")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_IE = transporta[j].InnerText;

                                                else if (transporta[j].Name == "xEnder")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_xEnder = transporta[j].InnerText;

                                                else if (transporta[j].Name == "xMun")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_xMun = transporta[j].InnerText;

                                                else if (transporta[j].Name == "UF")
                                                    xml.nfeProc_NFe_infNFe_total_transp_transporta_UF = transporta[j].InnerText;

                                                else
                                                    NaoRegistrado(transporta[j].Name, transporta[j].InnerText, xml);
                                            }
                                        }

                                        else if (transp[i].Name == "veicTransp")
                                        {
                                            XmlNodeList veicTransp = transp[i].ChildNodes;

                                            for (int j = 0; j < veicTransp.Count; j++)
                                            {
                                                if (veicTransp[j].Name == "placa")
                                                    xml.nfeProc_NFe_infNFe_total_transp_veicTransp_placa = veicTransp[j].InnerText;

                                                else if (veicTransp[j].Name == "UF")
                                                    xml.nfeProc_NFe_infNFe_total_transp_veicTransp_UF = veicTransp[j].InnerText;

                                                else
                                                    NaoRegistrado(veicTransp[j].Name, veicTransp[j].InnerText, xml);
                                            }
                                        }

                                        else if (transp[i].Name == "vol")
                                        {
                                            XmlNodeList vol = transp[i].ChildNodes;

                                            for (int j = 0; j < vol.Count; j++)
                                            {
                                                if (vol[j].Name == "esp")
                                                    xml.nfeProc_NFe_infNFe_total_transp_vol_esp = vol[j].InnerText;

                                                else if (vol[j].Name == "qVol")
                                                    xml.nfeProc_NFe_infNFe_total_transp_vol_qVol = Int32.Parse(vol[j].InnerText);

                                                else if (vol[j].Name == "marca")
                                                    xml.nfeProc_NFe_infNFe_total_transp_vol_marca = vol[j].InnerText;

                                                else if (vol[j].Name == "pesoL")
                                                    xml.nfeProc_NFe_infNFe_total_transp_vol_pesoL = double.Parse(vol[j].InnerText);

                                                else if (vol[j].Name == "pesoB")
                                                    xml.nfeProc_NFe_infNFe_total_transp_vol_pesoB = double.Parse(vol[j].InnerText);

                                                else
                                                    NaoRegistrado(vol[j].Name, vol[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(transp[i].Name, transp[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "cobr")
                                {
                                    XmlNodeList cobr = infNFe[h].ChildNodes;

                                    for (int i = 0; i < cobr.Count; i++)
                                    {
                                        if (cobr[i].Name == "fat")
                                        {
                                            XmlNodeList fat = cobr[i].ChildNodes;

                                            for (int j = 0; j < fat.Count; j++)
                                            {
                                                if (fat[j].Name == "nFat")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_fat_nFat = double.Parse(fat[j].InnerText);

                                                else if (fat[j].Name == "vOrig")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_fat_vOrig = double.Parse(fat[j].InnerText);

                                                else if (fat[j].Name == "vDesc")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_fat_vDesc = double.Parse(fat[j].InnerText);

                                                else if (fat[j].Name == "vLiq")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_fat_vLiq = double.Parse(fat[j].InnerText);

                                                else
                                                    NaoRegistrado(fat[j].Name, fat[j].InnerText, xml);
                                            }
                                        }

                                        else if (cobr[i].Name == "dup")
                                        {
                                            XmlNodeList dup = cobr[i].ChildNodes;

                                            for (int j = 0; j < dup.Count; j++)
                                            {
                                                if (dup[j].Name == "nDup")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_dup_nDup = dup[j].InnerText;

                                                else if (dup[j].Name == "dVenc")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_dup_dVenc = dup[j].InnerText;

                                                else if (dup[j].Name == "vDup")
                                                    xml.nfeProc_NFe_infNFe_total_cobr_dup_vDup = double.Parse(dup[j].InnerText);

                                                else
                                                    NaoRegistrado(dup[j].Name, dup[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(cobr[i].Name, cobr[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "pag")
                                {
                                    XmlNodeList pag = infNFe[h].ChildNodes;

                                    for (int i = 0; i < pag.Count; i++)
                                    {
                                        if (pag[i].Name == "detPag")
                                        {
                                            XmlNodeList detPag = pag[i].ChildNodes;

                                            for (int j = 0; j < detPag.Count; j++)
                                            {
                                                if (detPag[j].Name == "indPag")
                                                    xml.nfeProc_NFe_infNFe_total_pag_detPag_indPag = Int32.Parse(detPag[j].InnerText);

                                                else if (detPag[j].Name == "tPag")
                                                    xml.nfeProc_NFe_infNFe_total_pag_detPag_tPag = Int32.Parse(detPag[j].InnerText);

                                                else if (detPag[j].Name == "vPag")
                                                    xml.nfeProc_NFe_infNFe_total_pag_detPag_vPag = double.Parse(detPag[j].InnerText);

                                                else
                                                    NaoRegistrado(detPag[j].Name, detPag[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(pag[i].Name, pag[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "infAdic")
                                {
                                    XmlNodeList infAdic = infNFe[h].ChildNodes;

                                    for (int i = 0; i < infAdic.Count; i++)
                                    {
                                        if (infAdic[i].Name == "infCpl")
                                            xml.nfeProc_NFe_infNFe_infAdic_infCpl = infAdic[i].InnerText;

                                        else
                                            NaoRegistrado(infAdic[i].Name, infAdic[i].InnerText, xml);
                                    }
                                }

                                else if (infNFe[h].Name == "infRespTec")
                                {
                                    XmlNodeList infRespTec = infNFe[h].ChildNodes;

                                    for (int i = 0; i < infRespTec.Count; i++)
                                    {
                                        if (infRespTec[i].Name == "CNPJ")
                                            xml.nfeProc_NFe_infNFe_infRespTec_CNPJ = infRespTec[i].InnerText;

                                        else if (infRespTec[i].Name == "xContato")
                                            xml.nfeProc_NFe_infNFe_infRespTec_xContato = infRespTec[i].InnerText;

                                        else if (infRespTec[i].Name == "email")
                                            xml.nfeProc_NFe_infNFe_infRespTec_email = infRespTec[i].InnerText;

                                        else if (infRespTec[i].Name == "fone")
                                            xml.nfeProc_NFe_infNFe_infRespTec_fone = infRespTec[i].InnerText;

                                        else
                                            NaoRegistrado(infRespTec[i].Name, infRespTec[i].InnerText, xml);
                                    }
                                }

                                else
                                    NaoRegistrado(infNFe[h].Name, infNFe[h].InnerText, xml);
                            }
                        }

                        else if (NFe[g].Name == "Signature")
                        {
                            XmlAttributeCollection SignatureAt = NFe[g].Attributes;

                            for (int h = 0; h < SignatureAt.Count; h++)
                            {
                                if (SignatureAt[h].Name == "xmlns")
                                    xml.nfeProc_NFe_Signature____xmlns = SignatureAt[h].InnerText;
                            }

                            XmlNodeList Signature = NFe[g].ChildNodes;

                            for (int h = 0; h < Signature.Count; h++)
                            {
                                if (Signature[h].Name == "SignedInfo")
                                {
                                    XmlAttributeCollection SignedInfoAt = Signature[h].Attributes;

                                    for (int i = 0; i < SignedInfoAt.Count; i++)
                                    {
                                        if (SignedInfoAt[i].Name == "xmlns")
                                            xml.nfeProc_NFe_Signature_SignedInfo____xmlns = SignedInfoAt[i].InnerText;
                                    }

                                    XmlNodeList SignedInfo = Signature[h].ChildNodes;

                                    for (int i = 0; i < SignedInfo.Count; i++)
                                    {
                                        if (SignedInfo[i].Name == "CanonicalizationMethod")
                                        {
                                            XmlAttributeCollection CanonicalizationMethodAt = SignedInfo[i].Attributes;

                                            for (int j = 0; j < CanonicalizationMethodAt.Count; j++)
                                            {
                                                if (CanonicalizationMethodAt[j].Name == "Algorithm")
                                                    xml.nfeProc_NFe_Signature_SignedInfo_CanonicalizationMethod____Algorithm = CanonicalizationMethodAt[j].InnerText;

                                                else
                                                    NaoRegistrado(CanonicalizationMethodAt[j].Name, CanonicalizationMethodAt[j].InnerText, xml);
                                            }
                                        }

                                        else if (SignedInfo[i].Name == "SignatureMethod")
                                        {
                                            XmlAttributeCollection SignatureMethodAt = SignedInfo[i].Attributes;

                                            for (int j = 0; j < SignatureMethodAt.Count; j++)
                                            {
                                                if (SignatureMethodAt[j].Name == "Algorithm")
                                                    xml.nfeProc_NFe_Signature_SignedInfo_SignatureMethod____Algorithm = SignatureMethodAt[j].InnerText;

                                                else
                                                    NaoRegistrado(SignatureMethodAt[j].Name, SignatureMethodAt[j].InnerText, xml);
                                            }
                                        }

                                        else if (SignedInfo[i].Name == "Reference")
                                        {
                                            XmlAttributeCollection ReferenceAt = SignedInfo[i].Attributes;

                                            for (int j = 0; j < ReferenceAt.Count; j++)
                                            {
                                                if (ReferenceAt[j].Name == "URI")
                                                    xml.nfeProc_NFe_Signature_SignedInfo_Reference____URI = ReferenceAt[j].InnerText;
                                            }

                                            XmlNodeList Reference = SignedInfo[i].ChildNodes;

                                            for (int j = 0; j < Reference.Count; j++)
                                            {
                                                if (Reference[j].Name == "Transforms")
                                                {
                                                    XmlNodeList Transforms = Reference[j].ChildNodes;

                                                    for (int k = 0; k < Transforms.Count; k++)
                                                    {
                                                        if (Transforms[k].Name == "Transform")
                                                        {
                                                            XmlFileAlgorithm xmlAlg = new XmlFileAlgorithm();
                                                            XmlAttributeCollection TransformAt = Transforms[k].Attributes;

                                                            for (int l = 0; l < TransformAt.Count; l++)
                                                            {
                                                                if (TransformAt[l].Name == "Algorithm")
                                                                    xmlAlg.nfeProc_NFe_Signature_SignedInfo_Reference_Transforms_Transform____Algorithm = TransformAt[l].InnerText;
                                                            }

                                                            xmlAlg.XmlFileId = xml.nfeProc_NFe_infNFe_____Id;
                                                            context.Algorithms.Add(xmlAlg);
                                                        }

                                                        else
                                                            NaoRegistrado(Transforms[k].Name, Transforms[k].InnerText, xml);
                                                    }
                                                }

                                                else if (Reference[j].Name == "DigestMethod")
                                                {
                                                    XmlAttributeCollection DigestMethodAt = Reference[j].Attributes;

                                                    for (int k = 0; k < DigestMethodAt.Count; k++)
                                                    {
                                                        if (DigestMethodAt[k].Name == "Algorithm")
                                                            xml.nfeProc_NFe_Signature_SignedInfo_Reference_DigestMethod____Algorithm = DigestMethodAt[k].InnerText;
                                                    }
                                                }

                                                else if (Reference[j].Name == "DigestValue")
                                                    xml.nfeProc_NFe_Signature_SignedInfo_Reference_DigestValue = Reference[j].InnerText;

                                                else
                                                    NaoRegistrado(Reference[j].Name, Reference[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(SignedInfo[i].Name, SignedInfo[i].InnerText, xml);
                                    }
                                }

                                else if (Signature[h].Name == "SignatureValue")
                                {
                                    XmlAttributeCollection SignatureValueAt = Signature[h].Attributes;

                                    for (int i = 0; i < SignatureValueAt.Count; i++)
                                    {
                                        if (SignatureValueAt[i].Name == "xmlns")
                                            xml.nfeProc_NFe_Signature_SignatureValue____xmlns = SignatureValueAt[i].InnerText;
                                    }

                                    xml.nfeProc_NFe_Signature_SignatureValue = Signature[h].InnerText;
                                }

                                else if (Signature[h].Name == "KeyInfo")
                                {
                                    XmlAttributeCollection KeyInfoAt = Signature[h].Attributes;

                                    for (int i = 0; i < KeyInfoAt.Count; i++)
                                    {
                                        if (KeyInfoAt[i].Name == "xmlns")
                                            xml.nfeProc_NFe_Signature_KeyInfo____xmlns = KeyInfoAt[i].InnerText;
                                    }

                                    XmlNodeList KeyInfo = Signature[h].ChildNodes;

                                    for (int i = 0; i < KeyInfo.Count; i++)
                                    {
                                        if (KeyInfo[i].Name == "X509Data")
                                        {
                                            XmlNodeList X509Data = KeyInfo[i].ChildNodes;

                                            for (int j = 0; j < X509Data.Count; j++)
                                            {
                                                if (X509Data[j].Name == "X509Certificate")
                                                    xml.nfeProc_NFe_Signature_KeyInfo_X509Data_X509Certificate = X509Data[j].InnerText;

                                                else
                                                    NaoRegistrado(X509Data[j].Name, X509Data[j].InnerText, xml);
                                            }
                                        }

                                        else
                                            NaoRegistrado(KeyInfo[i].Name, KeyInfo[i].InnerText, xml);
                                    }
                                }

                                else
                                    NaoRegistrado(Signature[h].Name, Signature[h].InnerText, xml);
                            }
                        }

                        else
                            NaoRegistrado(NFe[g].Name, NFe[g].InnerText, xml);
                    }
                }

                else if (nfeProc[f].Name == "protNFe")
                {
                    XmlAttributeCollection protNFeAt = nfeProc[f].Attributes;

                    for (int g = 0; g < protNFeAt.Count; g++)
                    {
                        if (protNFeAt[g].Name == "xmlns")
                            xml.nfeProc_protNFe____xmlns = protNFeAt[g].InnerText;

                        else if (protNFeAt[g].Name == "versao")
                            xml.nfeProc_protNFe____versao = protNFeAt[g].InnerText;
                    }

                    XmlNodeList protNFe = nfeProc[f].ChildNodes;

                    for (int g = 0; g < protNFe.Count; g++)
                    {
                        if (protNFe[g].Name == "infProt")
                        {
                            XmlAttributeCollection infProtAt = protNFe[g].Attributes;

                            for (int h = 0; h < infProtAt.Count; h++)
                            {
                                if (infProtAt[h].Name == "xmlns")
                                    xml.nfeProc_protNFe_infProt____xmlns = infProtAt[h].InnerText;

                                else if (infProtAt[h].Name == "Id")
                                    xml.nfeProc_protNFe_infProt____Id = infProtAt[h].InnerText;
                            }

                            XmlNodeList infProt = protNFe[g].ChildNodes;

                            for (int h = 0; h < infProt.Count; h++)
                            {
                                if (infProt[h].Name == "tpAmb")
                                    xml.nfeProc_protNFe_infProt_tpAmb = Int32.Parse(infProt[h].InnerText);

                                else if (infProt[h].Name == "verAplic")
                                    xml.nfeProc_protNFe_infProt_verAplic = infProt[h].InnerText;

                                else if (infProt[h].Name == "chNFe")
                                    xml.nfeProc_protNFe_infProt_chNFe = infProt[h].InnerText;

                                else if (infProt[h].Name == "dhRecbto")
                                    xml.nfeProc_protNFe_infProt_dhRecbto = infProt[h].InnerText;

                                else if (infProt[h].Name == "nProt")
                                    xml.nfeProc_protNFe_infProt_nProt = infProt[h].InnerText;

                                else if (infProt[h].Name == "digVal")
                                    xml.nfeProc_protNFe_infProt_digVal = infProt[h].InnerText;

                                else if (infProt[h].Name == "cStat")
                                    xml.nfeProc_protNFe_infProt_cStat = Int32.Parse(infProt[h].InnerText);

                                else if (infProt[h].Name == "xMotivo")
                                    xml.nfeProc_protNFe_infProt_xMotivo = infProt[h].InnerText;

                                else
                                    NaoRegistrado(infProt[h].Name, infProt[h].InnerText, xml);
                            }
                        }

                        else
                            NaoRegistrado(protNFe[g].Name, protNFe[g].InnerText, xml);
                    }
                }

                else
                    NaoRegistrado(nfeProc[f].Name, nfeProc[f].InnerText, xml);
            }
        }


        private void Move(string arq, string folder)
        {
            var destinationFile = Path.Combine(folder, Path.GetFileName(arq.Split('\\')[6]));

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            File.Move(arq, destinationFile);
        }


        private void NaoRegistrado(string element, string text, XmlFile xml)
        {
            XmlFileNaoRegistrado nr = new XmlFileNaoRegistrado();
            string nome = "<" + element + ">" + " " + text;
            nr.InfoNaoRegistrada = nome;
            nr.XmlFileId = xml.nfeProc_NFe_infNFe_____Id;
            context.NRs.Add(nr);
            Log.Warning("Uma informação não registrada foi encontrada no arquivo xml: " + xml.XmlName);
        }
    }
}
