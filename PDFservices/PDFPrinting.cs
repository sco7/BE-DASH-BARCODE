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
        public static Task<bool> PDFToPrinter(string filePath, string printerName)
        {
            //var t = new TaskCompletionSource<bool>();

            //filePath = filePath.Replace('\\', '/');

            //string fullFilePath = Path.GetFullPath(filePath);

            //fullFilePath = fullFilePath.Replace("\\", "\\\\");

            //Console.WriteLine($"FoxitReader.exe /t {fullFilePath} \"{printerName}\"");

            //var process = new Process
            //{
            //    StartInfo = {
            //        FileName = "FoxitReader.exe",
            //        Arguments = $"/t \"{fullFilePath}\" \"{printerName}\"",
            //        UseShellExecute = false,
            //        WindowStyle = ProcessWindowStyle.Hidden
            //    },
            //    EnableRaisingEvents = true
            //};

            //    //process.ErrorDataReceived += (s, e) =>
            //    //{
            //    //    Console.WriteLine("Error, " + e.Data);
            //    //};

            //process.Exited += (s, e) =>
            //{
            //    process.Dispose();
            //    t.SetResult(process.);
            //};

            //process.Start();
            //process.BeginOutputReadLine();
            //return t.Task;

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
                //Console.WriteLine($"Exitted: {x.ExitCode}");
                x.Dispose();
                t.SetResult(true);
            };

            x.Start();
            return t.Task;

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
