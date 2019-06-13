using Neodynamic.SDK.Printing;
using System;

namespace FontaineVerificationProject.Export
{
    public class Export
    {
        public static string ExportToPDF(ThermalLabel tLabel, string filePath)
        {      
            using (PrintJob pj = new PrintJob())
            {
                pj.ThermalLabel = tLabel;
                pj.ExportToPdf(filePath, 203);
            }
            return filePath;
        }

        public static void ExportToPNG(ThermalLabel tLabel, string filepath)
        {
            using (PrintJob pj = new PrintJob())
            {
                pj.ThermalLabel = tLabel;
                pj.ExportToImage(filepath + ".png", new ImageSettings(ImageFormat.Png), 200);
            }
        }

        public static void Print(ThermalLabel tLabel, int copies, string printerName)
        {
            while (copies > 0) {
                using (WindowsPrintJob pj = new WindowsPrintJob())
                {
                    pj.PrinterSettings = new PrinterSettings { PrinterName = printerName, Dpi = 203, ProgrammingLanguage = ProgrammingLanguage.ZPL, UseDefaultPrinter = false };
                    pj.ThermalLabel = tLabel;
                    pj.Print();
                }
                copies --;
            }
        }      
    }
}