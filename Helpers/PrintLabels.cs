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
            
            Array.ForEach(Directory.GetFiles("@C:\\Users\\user\\source\\repos\\Fontaine\\FontaineBackend\\PDFservices\\Data"), File.Delete);
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
                    //var filepath1 = @"C:\Users\user\Documents\Scott\Fontaine\Label1\" + chassisNo + ".pdf";
                    var filepath1 = @"C:\Users\user\source\repos\Fontaine\FontaineBackend\PDFservices\Data\Label1-" + chassisNo + ".pdf";
                    
                    ThermalLabel tLabel2 = new V2Label(stockCode, chassisNo);
                    //var filepath2 = @"C:\Users\user\Documents\Scott\Fontaine\Label2\" + serialNo + ".pdf";
                    var filepath2 = @"C:\Users\user\source\repos\Fontaine\FontaineBackend\PDFservices\Data\Label2-" + chassisNo + ".pdf";
                    
                    // Export to PDF
                    Export.Export.ExportToPDF(tLabel1, filepath1, LabelprinterName, tLabel1copies);      
                    Export.Export.ExportToPDF(tLabel2, filepath2, LabelprinterName, tLabel2copies);

                    c++;

                    // Print - Does not work in .Net
                    //Export.Export.Print(tLabel1, tLabel1copies, Label1printerName);
                    //Export.Export.Print(tLabel2, tLabel2copies, Label2printerName);
            }
        }
    }
}