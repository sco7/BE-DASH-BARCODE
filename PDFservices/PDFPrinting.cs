using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FontaineVerificationProject.PrintingService
{
    public class PDFPrinting
    {
        public static void PDFToPrinter(string filePath, string printerName, int copies)
        {
            filePath = filePath.Replace('\\', '/');
            while (copies > 0)
            {
                // Thread.Sleep(2000);

                Console.WriteLine($"PDFtoPrinter.exe {filePath} \"{printerName}\"");

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

        public async static Task SendRawCmdToPrinter(string filepath, string printerName)
        {
            var t = new TaskCompletionSource<bool>();

            using (var process = new Process
            {
                StartInfo = {
                    FileName = "cmd.exe",
                    Arguments = $"/C copy /B {filepath} \"\\\\localhost\\{printerName}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                },
                EnableRaisingEvents = true
            })
            {
                Console.WriteLine($"{process.StartInfo.FileName} {process.StartInfo.Arguments}");

                process.Exited += (sender, args) =>
                {
                    Console.WriteLine("Ended, " + args.GetType().Name);
                };

                process.Start();
                process.BeginOutputReadLine();
                await t.Task;
            }
        }
    }
}
