using Neodynamic.SDK.Printing;
using FontaineVerificationProject.PrintingService;

namespace FontaineVerificationProject.Export
{
    public class Export
    {
        public static void ExportToPDF(ThermalLabel tLabel, string filePath, string printerName, int copies)
        {                
            using (PrintJob pj = new PrintJob())
            {
                pj.ThermalLabel = tLabel;
                pj.ExportToPdf(filePath, 203);
            }
            PDFPrinting.PDFToPrinter(filePath, printerName, copies);
        }
    }
}