using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Models;
using Neodynamic.SDK.Printing;
using FontaineVerificationProject.Labels;

namespace FontaineVerificationProject.Helpers
{
    public class PrintLabels
    {
        public void PrintDespatchLabels(List<Sale> data) {
            
            foreach (var i in data) 
            {
                    //int tLabel1copies = 2;
                    //int tLabel2copies = 1;
                    //string tLabel1printerName = "T2N";
                    //string tLabel2printerName = "T2N";

                    int customerProductNo = i.CustomerProductNo; 
                    string description = i.Description;
                    int chassisNo = i.ChassisNo;
                    int serialNo = i.SerialNo; 
                                    
                    ThermalLabel tLabel1 = new V1Label(customerProductNo, description, chassisNo);
                    var filepath1 = @"C:\Users\user\Documents\Scott\Fontaine\Label1\" + chassisNo + ".pdf";

                    ThermalLabel tLabel2 = new V2Label(serialNo, chassisNo);
                    var filepath2 = @"C:\Users\user\Documents\Scott\Fontaine\Label2\" + serialNo + ".pdf";
                    
                    // Export to PDF
                    Export.Export.ExportToPDF(tLabel1, filepath1);
                    Export.Export.ExportToPDF(tLabel2, filepath2);

                    // Print
                    //Export.Export.Print(tLabel1, tLabel1copies, tLabel1printerName);
                    //Export.Export.Print(tLabel2, tLabel2copies, tLabel2printerName);        
            }
        }
    }
}