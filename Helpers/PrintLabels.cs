using System;
using System.Collections.Generic;
using FontaineVerificationProject.Models;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;
using System.IO;


namespace FontaineVerificationProject.Helpers
{
    public class PrintLabels
    {
        public void PrintDespatchLabels(List<vGetChassisNumbers> data) {

            // Delete existing labels files
            DirectoryInfo di = new DirectoryInfo(@"PDFservices\Data\");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            
            int c = 0;
            foreach (var i in data) 
            {
                    int tLabel1copies = 2;
                    int tLabel2copies = 1;
                    string LabelprinterName = "SATO WS408";

                    string customerProductNo = i.MCusSupStkCode; 
                    string stockDescription = i.MStockDes;
                    string chassisNo = i.ChassisNumber;
                    string stockCode = i.MStockCode;
                    DateTime despatchDate = i.MLineShipDate; 
                                    
                    ThermalLabel tLabel1 = new V1Label(customerProductNo, stockDescription, chassisNo, despatchDate);
                    var filepath1 = @"PDFservices\Data\Label1-" + chassisNo + ".pdf";
                    var filepath1Full = Path.GetFullPath(filepath1);
                    
                    
                    ThermalLabel tLabel2 = new V2Label(stockCode, chassisNo);
                    var filepath2 = @"PDFservices\Data\Label2-" + chassisNo + ".pdf";
                    var filepath2Full = Path.GetFullPath(filepath2);
                    
                    // Export to PDF
                    Export.Export.ExportToPDF(tLabel1, filepath1Full, LabelprinterName, tLabel1copies);      
                    Export.Export.ExportToPDF(tLabel2, filepath2Full, LabelprinterName, tLabel2copies);

                    c++;

                    // Print - Does not work in .Net
                    //Export.Export.Print(tLabel1, tLabel1copies, Label1printerName);
                    //Export.Export.Print(tLabel2, tLabel2copies, Label2printerName);
            }
        }
    }
}