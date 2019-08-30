using System;
using System.Collections.Generic;
using FontaineVerificationProject.Models;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;
using System.IO;
using System.Linq;
using FontaineVerificationProjectBack.PDFservices;
using FontaineVerificationProjectBack.Models;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Helpers
{
    public class PrintLabels
    {
        public async Task PrintDespatchLabels(List<vGetChassisNumbers> data, PrintingConfig _printing)
        {  
            DirectoryInfo di = new DirectoryInfo(@"PDFservices\Data\");
            
            int c = 0;
            List<vGetChassisNumbers> sortedData = data.OrderBy(x => x.ChassisNumber).ToList();
            List<string> pdfPagesLabel1 = new List<string>();
            List<string> pdfPagesLabel2 = new List<string>();

            string LabelprinterName = _printing.LabelPrinterName; //"SATO WS408"; 
            string mergedLabels1 = "";
            string mergedLabels2 = "";
            bool usePdf = true;

            foreach (var i in sortedData) 
            {
                int tLabel1copies = 2;
                int tLabel2copies = 2;
                string customerProductNo = i.MCusSupStkCode; 
                string stockDescription = i.MStockDes;
                string chassisNo = i.ChassisNumber;
                string stockCode = i.MStockCode;
                DateTime despatchDate = i.MLineShipDate;

                ThermalLabel tLabel1 = new V1Label(customerProductNo, stockDescription, chassisNo, despatchDate);                
                ThermalLabel tLabel2 = new V2Label(stockCode, customerProductNo);

                var dir = Path.Combine("PDFservices", "Data");

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (usePdf)
                {
                    var filepath1 = Path.Combine(dir, Guid.NewGuid() + ".pdf");
                    var filepath2 = Path.Combine(dir, Guid.NewGuid() + ".pdf");

                    while (tLabel1copies > 0)
                    {
                        pdfPagesLabel1.Add(Export.Export.ExportToPDF(tLabel1, filepath1));
                        tLabel1copies --;
                    }

                    while (tLabel2copies > 0)
                    {
                        pdfPagesLabel2.Add(Export.Export.ExportToPDF(tLabel2, filepath2));
                        tLabel2copies --;
                    }
                }
                else
                {
                    var filepath1 = Path.Combine(dir, Guid.NewGuid() + ".pdf");
                    var filepath2 = Path.Combine(dir, Guid.NewGuid() + ".pdf");

                Export.Export.PrintZpl(tLabel1, filepath1, LabelprinterName, tLabel1copies);
                    Export.Export.PrintZpl(tLabel2, filepath2, LabelprinterName, tLabel2copies);
                }
                c++;                    
            }
            String[] pdfLabelArr1 = pdfPagesLabel1.ToArray();
            String[] pdfLabelArr2 = pdfPagesLabel2.ToArray();

            if (usePdf)
            {
                mergedLabels1 = PDFSharp.MergeMultiplePDFIntoSinglePDF(pdfLabelArr1, Guid.NewGuid().ToString());
                mergedLabels2 = PDFSharp.MergeMultiplePDFIntoSinglePDF(pdfLabelArr2, Guid.NewGuid().ToString());

                await PrintingService.PDFPrinting.PDFToPrinter(mergedLabels1, LabelprinterName);
                await PrintingService.PDFPrinting.PDFToPrinter(mergedLabels2, LabelprinterName);
            }

            File.Delete(mergedLabels1);
            File.Delete(mergedLabels2);
            foreach (var f in pdfPagesLabel1) File.Delete(f);
            foreach (var f in pdfPagesLabel2) File.Delete(f);
            foreach (var f in pdfLabelArr1) File.Delete(f);
            foreach (var f in pdfLabelArr2) File.Delete(f);
        }
    }
}