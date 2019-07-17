using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProjectBack.PDFservices
{
    public class PDFSharp
    {
        public static string MergeMultiplePDFIntoSinglePDF(string[] pdfFiles, string label)
        {
            if (pdfFiles.Length == 0) throw new Exception("No data supplied to generate pdf");

            string outputFilePath = @"PDFservices\Data\" + label + ".pdf";

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

