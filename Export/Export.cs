using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FontaineVerificationProject.PrintingService;
using Neodynamic.SDK.Printing;

namespace FontaineVerificationProject.Export
{
    public class Export
    {

        public static String ExportToPDF(ThermalLabel tLabel, string filePath)
        {                
            using (PrintJob pj = new PrintJob())
            {
                pj.ThermalLabel = tLabel;
                pj.ExportToPdf(filePath, 203);
            }
            return filePath;
        }

        public async static void PrintZpl(ThermalLabel label, string filePath, string printerName, int copies)
        {
            string zpl = "";
            using (var pj = new PrintJob())
            {
                pj.ThermalLabel = label;
                pj.ProgrammingLanguage = ProgrammingLanguage.ZPL;
                zpl = pj.GetNativePrinterCommands();
            }

            if (!System.IO.Directory.Exists("PDFservices\\Data"))
            {
                System.IO.Directory.CreateDirectory("PDFservices\\Data");
            }

            string filepath = System.IO.Path.Combine("PDFservices\\Data", Guid.NewGuid() + ".zpl");

            System.IO.File.WriteAllText(filePath, zpl);

            while (copies > 0)
            {
                Console.WriteLine("sending to printer");
                await PDFPrinting.SendRawCmdToPrinter(filePath, printerName);
                copies--;
            }
        }

    }
}