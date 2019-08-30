using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FontaineVerificationProject.PrintingService
{
    public class PDFPrinting
    {
        public static Task<bool> PDFToPrinter(string filePath, string printerName)
        {        
            var t = new TaskCompletionSource<bool>();
            filePath = filePath.Replace('\\', '/');
            var x = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    Arguments = $"/t \"{filePath}\" \"{printerName}\"",
                    FileName = "foxitreader.exe"
                },
                EnableRaisingEvents = true
            };

            x.Exited += (s, e) =>
            {
                x.Dispose();
                t.SetResult(true);
            };

            x.Start();
            return t.Task;
        }

        // task not required, PDFtoPrinter to be used instead
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
