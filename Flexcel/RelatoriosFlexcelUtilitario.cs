using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FlexCel.XlsAdapter;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;

namespace ApiAcme.Flexcel
{
    public class RelatoriosFlexcelUtilitario
    {
        FlexCel.Report.FlexCelReport Report = null;
        XlsFile ReportFile = null;
        MemoryStream StreamRelatorio = null;

        public void GerarExcel<T>(IList<T> aLista, string NomeFonte, string aCaminhoTemplate, string acaminhoArquivoSaida)
        {
            try
            {
                Report = new FlexCel.Report.FlexCelReport(true);
                ReportFile = new FlexCel.XlsAdapter.XlsFile(true);
                StreamRelatorio = new MemoryStream();

                SetReport(aLista, NomeFonte);

                this.CreateFile(ReportFile, aCaminhoTemplate);

                Report.Run(ReportFile);

                ReportFile.Save(acaminhoArquivoSaida);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetReport<T>(IList<T> aLista, string NomeFonte)
        {
            Report.SetValue("Empresa", "LCorp S.A.");
            Report.SetValue("PaginaXdeY", string.Empty);
            Report.SetValue("DataSistema", DateTime.Now.ToShortDateString());

            Report.AddTable(NomeFonte, aLista);

            //Adiciona a função de BuscarContexto, para que possam ser traduzido os termos
            Report.SetUserFunction("BuscarContexto", new BuscarContexto());
        }

        private void CreateFile(FlexCel.Core.ExcelFile xls, string aCaminhoTemplate)
        {
            xls.Open(aCaminhoTemplate);    //Open an existing xls file for modification.
            xls.ActiveSheet = 1;    //Set the sheet we are working in.
        }

        private void AdicionarTabela<T>(string aTabela, IList<T> aSource)
        {
            this.Report.AddTable(aTabela, aSource);
        }

        public void ExportToPdf(string inFile, string outFile)
        {
            XlsFile xls = new XlsFile(inFile);

            using (var pdf = new FlexCel.Render.FlexCelPdfExport(xls, true))
            {
                pdf.Export(outFile);
            }
        }

        public void ExportToHtml(string inFile, string outFile)
        {
            XlsFile xls = new XlsFile(inFile);

            using (var html = new FlexCel.Render.FlexCelHtmlExport(xls, true))
            {
                html.Export(outFile, null);
            }
        }
    }

    public class BuscarContexto : FlexCel.Report.TFlexCelUserFunction
    {
        List<ResourceProjecao> MapaContexto = new List<ResourceProjecao>();

        public override object Evaluate(object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return "";
            if (MapaContexto.Count == 0)
            {
                GerarMapaContexto(parameters);
            }

            return MapaContexto.Where(a => a.Chave == parameters[0].ToString()).FirstOrDefault().Valor;

        }

        private void GerarMapaContexto(object[] parameters)
        {
            //Gerar mapa a partir de um .resx  resgen TraducaoPTBR.resx Traducao.resources
            using (ResourceReader ResReader = new ResourceReader(@".\RelatoriosTemplates\Traducao.resources"))
            {
                IDictionaryEnumerator tDictEnum = ResReader.GetEnumerator();

                //tDictEnum.RetornarValor(parameters[0].ToString());

                while (tDictEnum.MoveNext())
                {
                    MapaContexto.Add(new ResourceProjecao() { Chave = tDictEnum.Key.ToString(), Valor = tDictEnum.Value.ToString() });
                }

                ResReader.Close();
            }
        }
    }

    public class ResourceProjecao
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }

    public static class UtilIDictionary
    {
        public static IEnumerable<KeyValuePair<object, object>> RetornarValor(this IDictionaryEnumerator iter, string aValor)
        {
            using (iter as IDisposable)
            {
                while (iter.MoveNext()) yield return new KeyValuePair<object, object>(iter.Key, iter.Value);
            }
        }
    }  

}
