using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text;
using System.IO;

public static class PDFMerger
{
    public static byte[] MergeFiles(List<byte[]> sourceFiles)
    {
        Document document = new Document();
        using (MemoryStream ms = new MemoryStream())
        {
            PdfCopy copy = new PdfCopy(document, ms);
            document.Open();
            int documentPageCounter = 0;

            // Iterate through all pdf documents
            for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
            {
                // Create pdf reader
                PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                int numberOfPages = reader.NumberOfPages;

                // Iterate through all pages
                for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                {
                    documentPageCounter++;
                    PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                    PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                    // Write header
                    ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                        importedPage.Width < importedPage.Height ? 0 : 1);

                    // Write footer
                    ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                        importedPage.Width < importedPage.Height ? 0 : 1);

                    pageStamp.AlterContents();

                    copy.AddPage(importedPage);
                }

                copy.FreeReader(reader);
                reader.Close();
            }

            document.Close();
            return ms.GetBuffer();
        }
    }

    public static void MergePages(string outputPdfPath, string[] lstFiles)
    {
        PdfReader reader = null;
        Document sourceDocument = null;
        PdfCopy pdfCopyProvider = null;
        PdfImportedPage importedPage;
        sourceDocument = new Document();
        pdfCopyProvider = new PdfCopy(sourceDocument,
        new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));
        sourceDocument.Open();
        try
        {
            for (int f = 0; f <= lstFiles.Length - 1; f++)
            {
                int pages = 1;
                reader = new PdfReader(lstFiles[f]);
                //Add pages of current file
                for (int i = 1; i <= pages; i++)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }
                reader.Close();
            }
            sourceDocument.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool MergePDFs(IEnumerable<string> fileNames, string targetPdf)
    {
        bool merged = true;
        using (FileStream stream = new FileStream(targetPdf, FileMode.Create))
        {
            Document document = new Document();
            PdfCopy pdf = new PdfCopy(document, stream);
            PdfReader reader = null;
            try
            {
                document.Open();
                foreach (string file in fileNames)
                {
                    reader = new PdfReader(file);
                    pdf.AddDocument(reader);
                    reader.Close();
                }
            }
            catch (Exception)
            {
                merged = false;
                if (reader != null)
                {
                    reader.Close();
                }
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                }
            }
        }
        return merged;
    }

    //public static void CombineMultiplePDFs(string[] fileNames, string outFile)
    //{
    //    // step 1: creation of a document-object
    //    Document document = new Document();

    //    // step 2: we create a writer that listens to the document
    //    PdfCopy writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
    //    if (writer == null)
    //    {
    //        return;
    //    }

    //    // step 3: we open the document
    //    document.Open();

    //    foreach (string fileName in fileNames)
    //    {
    //        // we create a reader for a certain document
    //        PdfReader reader = new PdfReader(fileName);
    //        reader.ConsolidateNamedDestinations();

    //        // step 4: we add content
    //        for (int i = 1; i <= reader.NumberOfPages; i++)
    //        {
    //            PdfImportedPage page = writer.GetImportedPage(reader, i);
    //            writer.AddPage(page);
    //        }

    //        PRAcroForm form = reader.AcroForm;
    //        if (form != null)
    //        {
    //            writer.CopyAcroForm(reader);
    //        }

    //        reader.Close();
    //    }

    //    // step 5: we close the document and writer
    //    writer.Close();
    //    document.Close();
    //}
}