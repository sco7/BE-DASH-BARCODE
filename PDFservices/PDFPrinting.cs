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
        public async static void PDFToPrinter(string filePath, string printerName)
        {
            filePath = filePath.Replace('\\', '/');

            string fullFilePath = Path.GetFullPath(filePath);

            fullFilePath = fullFilePath.Replace("\\", "\\\\");

            Console.WriteLine($"FoxitReader.exe /t {fullFilePath} \"{printerName}\"");

            using (var process = new Process
            {
                StartInfo = {
                    FileName = "FoxitReader.exe",
                    Arguments = $"/t \"{fullFilePath}\" \"{printerName}\"",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                },
                EnableRaisingEvents = true
            })
            {

                process.ErrorDataReceived += (s, e) =>
                {
                    Console.WriteLine("Error, " + e.Data);
                };

                process.Exited += (s, e) =>
                {
                    Console.WriteLine("Ended, " + e.GetType().Name);
                };

                process.Start();


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
