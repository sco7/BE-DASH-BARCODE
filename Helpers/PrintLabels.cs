using System;
using System.Collections.Generic;
using FontaineVerificationProject.Models;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;
using System.IO;
using System.Linq;
using FontaineVerificationProjectBack.PDFservices;

namespace FontaineVerificationProject.Helpers
{
    public class PrintLabels
    {
        public void PrintDespatchLabels(List<vGetChassisNumbers> data) {

            // Delete existing labels files
            DirectoryInfo di = new DirectoryInfo(@"PDFservices\Data\");
            //foreach (FileInfo file in di.GetFiles())
            //{
            //    file.Delete(); 
            //}
            
            int c = 0;
            List<vGetChassisNumbers> sortedData = data.OrderBy(x => x.ChassisNumber).ToList();
            List<string> pdfPagesLabel1 = new List<string>();
            List<string> pdfPagesLabel2 = new List<string>();

            string LabelprinterName = "SatoZpl";  //"SATO WS408";  // "Brother HL-5240 series"
            string mergedLabels1 = "";
            string mergedLabels2 = "";
            bool usePdf = true;

            foreach (var i in sortedData) 
            {
                int tLabel1copies = 2;
                int tLabel2copies = 1;
                string customerProductNo = i.MCusSupStkCode; 
                string stockDescription = i.MStockDes;
                string chassisNo = i.ChassisNumber;
                string stockCode = i.MStockCode;
                DateTime despatchDate = i.MLineShipDate; 
                                    
                ThermalLabel tLabel1 = new V1Label(customerProductNo, stockDescription, chassisNo, despatchDate);                
                ThermalLabel tLabel2 = new V2Label(stockCode, chassisNo);
               
                if (usePdf)
                {
                    var filepath1 = @"PDFservices\Data\Label1-" + chassisNo + ".pdf";
                    var filepath2 = @"PDFservices\Data\Label2-" + chassisNo + ".pdf";

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
                    var filepath2 = @"PDFservices\Data\Label2-" + chassisNo + ".zpl";
                    var filepath1 = @"PDFservices\Data\Label1-" + chassisNo + ".zpl";

                    Export.Export.PrintZpl(tLabel1, filepath1, LabelprinterName, tLabel1copies);
                    Export.Export.PrintZpl(tLabel2, filepath2, LabelprinterName, tLabel2copies);
                }
              

                c++;                    
            }

            if (usePdf)
            {
                String[] pdfLabelArr1 = pdfPagesLabel1.ToArray();
                String[] pdfLabelArr2 = pdfPagesLabel2.ToArray();

                mergedLabels1 = PDFSharp.MergeMultiplePDFIntoSinglePDF(pdfLabelArr1, "Label1");
                mergedLabels2 = PDFSharp.MergeMultiplePDFIntoSinglePDF(pdfLabelArr2, "Label2");

                PrintingService.PDFPrinting.PDFToPrinter(mergedLabels1, LabelprinterName);
                PrintingService.PDFPrinting.PDFToPrinter(mergedLabels2, LabelprinterName);

            }

        }
    }
}