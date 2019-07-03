using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.PrintingService
{
    public class PDFPrinting
    {
        public static async Task PDFToPrinter(string filePath, string printerName, int copies)
        {
            
            while (copies > 0)
            {     
                var p = Process.Start(new ProcessStartInfo
                {
                    FileName = "PDFtoPrinter.exe",
                    Arguments = $"{filePath} \"{printerName}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });

                p.ErrorDataReceived += (s, e) =>
                {
                    Console.WriteLine("Error, " + e.Data);
                };

                p.Exited += (s, e) =>
                {
                    Console.WriteLine("Ended, " + e.GetType().Name);
                };

                copies--;
            }    
        }
    }
}