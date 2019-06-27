using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;
using FontaineVerificationProject.Controllers;

namespace FontaineVerificationProject.Helpers
{
    public class PrintLabels
    {
        public void PrintDespatchLabels(List<Sale> data) {

            data.OrderBy(x => x.ChassisNo);
            
            foreach (var i in data) 
            {
                    int tLabel1copies = 2;
                    int tLabel2copies = 1;
                    string LabelprinterName = "SATO WS408";

                    int customerProductNo = i.CustomerProductNo; 
                    string stockDescription = i.StockDescription;
                    int chassisNo = i.ChassisNo;
                    int stockCode = i.StockCode;
                    DateTime despatchDate = i.DispatchDate; 
                                    
                    ThermalLabel tLabel1 = new V1Label(customerProductNo, stockDescription, chassisNo, despatchDate);
                    //var filepath1 = @"C:\Users\user\Documents\Scott\Fontaine\Label1\" + chassisNo + ".pdf";
                    var filepath1 = @"C:\Users\user\source\repos\Fontaine\FontaineBackend\PDFservices\Data\Label1-" + chassisNo + ".pdf";
                    
                    ThermalLabel tLabel2 = new V2Label(stockCode, chassisNo);
                    //var filepath2 = @"C:\Users\user\Documents\Scott\Fontaine\Label2\" + serialNo + ".pdf";
                    var filepath2 = @"C:\Users\user\source\repos\Fontaine\FontaineBackend\PDFservices\Data\Label2-" + chassisNo + ".pdf";
                    
                    // Export to PDF
                    
                    Export.Export.ExportToPDF(tLabel1, filepath1, LabelprinterName, tLabel1copies);
                   
                    Export.Export.ExportToPDF(tLabel2, filepath2, LabelprinterName, tLabel2copies);

                    // Print - Does not work in .Net
                    //Export.Export.Print(tLabel1, tLabel1copies, Label1printerName);
                    //Export.Export.Print(tLabel2, tLabel2copies, Label2printerName);
            }
        }
    }
}