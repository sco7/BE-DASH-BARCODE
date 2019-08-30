using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace FontaineVerificationProjectBack.PDFservices
{
  public class PDFSharp
    {
        public static string MergeMultiplePDFIntoSinglePDF(string[] pdfFiles, string label)
        {
            if (pdfFiles.Length == 0) throw new Exception("No data supplied to generate pdf");
            var dir = Path.Combine("PDFservices", "Data");

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string outputFilePath = Path.Combine(dir, label + ".pdf");

            PdfDocument outputPDFDocument = new PdfDocument();
            foreach (string pdfFile in pdfFiles)
            {
                PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                outputPDFDocument.Version = inputPDFDocument.Version;
                foreach (PdfPage page in inputPDFDocument.Pages)
                {
                    outputPDFDocument.AddPage(page);
                }
            }
            outputPDFDocument.Save(outputFilePath);
            return outputFilePath;
        }
    }
}

