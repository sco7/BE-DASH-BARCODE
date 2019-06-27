using Neodynamic.SDK.Printing;
using System.Printing;
using FontaineVerificationProject.PrintingService;

namespace FontaineVerificationProject.Export
{
    public class Export
    {
        public static async void ExportToPDF(ThermalLabel tLabel, string filePath, string printerName, int copies)
        {                
            using (PrintJob pj = new PrintJob())
            {
                pj.ThermalLabel = tLabel;
                pj.ExportToPdf(filePath, 203);
            }

            await PDFPrinting.PDFToPrinter(filePath, printerName, copies);
        }

    }
}