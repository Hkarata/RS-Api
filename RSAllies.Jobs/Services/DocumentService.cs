using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RSAllies.Jobs.Queries;



namespace RSAllies.Jobs.Services
{
    internal static class DocumentService
    {
        public static void GenerateSessionPdf(Guid sessionId, Venue venue, List<User> users)
        {

            var filename = $"{sessionId}.pdf";
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", filename);

            string currentDirectory = Directory.GetCurrentDirectory();
            string pdfDirectory = Path.Combine(currentDirectory, "PDFs");

            // Check if the directory exists
            if (!Directory.Exists(pdfDirectory))
            {
                // Create the directory if it does not exist
                Directory.CreateDirectory(pdfDirectory);
            }

            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontFamily("Fira Code"));

                    page.Content()
                        .Column(column =>
                        {
                            column.Item()
                            .PaddingBottom(0.5f, Unit.Centimetre)
                            .Text($"{venue.VenueName}")
                            .FontSize(18)
                            .FontColor("#d0e33e");

                            column.Item()
                            .PaddingBottom(0.2f, Unit.Centimetre)
                            .Row(row =>
                            {
                                row.RelativeItem()
                                .Text($"{venue.District}, {venue.Region}");

                                row.RelativeItem()
                                .Text($"{venue.Date.Date}")
                                .AlignRight();
                            });

                            column.Item()
                            .PaddingBottom(1, Unit.Centimetre)
                            .Row(row =>
                            {
                                row.RelativeItem()
                                .Text("List of Attendees");

                                row.RelativeItem()
                                .Text($"Number of Attendees: {venue.Capacity}")
                                .AlignRight();
                            });

                            column.Item()
                            .Table(table =>
                            {

                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(70);
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("S/No.")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("Name")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("Gender")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("NIN or Passport")
                                    .FontColor(Colors.White);

                                });

                                int i = 1;

                                foreach (var user in users)
                                {
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{i}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.FirstName} {user.MiddleName} {user.LastName}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.Gender}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.Identification}");
                                    i++;
                                }

                            });

                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf(pdfPath);
        }
    }
}
