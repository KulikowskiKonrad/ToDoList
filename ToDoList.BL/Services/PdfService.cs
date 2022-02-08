using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ToDoList.BL.ServiceInterfaces;

namespace ToDoList.BL.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf()
        {
            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument, PageSize.A4);
            var newLine = new Paragraph(new Text("\n"));

            document.SetTopMargin(150);

            var header = new Paragraph("Logo")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            var lineSeparator = new LineSeparator(new SolidLine());

            document.Add(header);
            document.Add(lineSeparator);

            var paragraph1 = new Paragraph("Lorem ipsum " +
                                           "dolor sit amet, consectetur adipiscing elit, " +
                                           "sed do eiusmod tempor incididunt ut labore " +
                                           "et dolore magna aliqua.");

            document.Add(paragraph1);

            var table = new Table(2, false);

            var cell11 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("State"));
            var cell12 = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Capital"));

            var cell21 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("New York"));
            var cell22 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Albany"));

            var cell31 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("New Jersey"));
            var cell32 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Trenton"));

            var cell41 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("California"));
            var cell42 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Sacramento"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);

            var nextPage = new AreaBreak(AreaBreakType.NEXT_PAGE);

            document.Add(nextPage);
            document.Add(table);

            var link = new Link("click here",
                PdfAction.CreateURI("https://www.google.com"));
            var hyperLink = new Paragraph("Please ")
                .Add(link.SetBold().SetUnderline()
                    .SetItalic().SetFontColor(ColorConstants.BLUE))
                .Add(" to go www.google.com.");

            document.Add(newLine);
            document.Add(hyperLink);
            var imageData = ImageDataFactory.Create($"{AppDomain.CurrentDomain.BaseDirectory}Images\\logo.jpg");
            float imageFitHeight = 80;
            var numberOfPages = pdfDocument.GetNumberOfPages();
            for (var i = 1; i <= numberOfPages; i++)
            {
                var image = new Image(imageData).ScaleAbsolute(100, imageFitHeight)
                    .SetFixedPosition(i, 25, PageSize.A4.GetHeight() - (imageFitHeight + 50));

                document.Add(image);

                document.ShowTextAligned(new Paragraph("page " + i + " of " + numberOfPages),
                    335, 50, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
            }

            document.Close();

            return stream.ToArray();
        }
    }
}