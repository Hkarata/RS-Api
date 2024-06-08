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
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(50);
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
                                    .Text("First Name")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("Middle Name")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("Last Name")
                                    .FontColor(Colors.White);

                                    header.Cell()
                                    .Background(Colors.DeepOrange.Medium)
                                    .Padding(1, Unit.Millimetre)
                                    .Text("ATD")
                                    .FontColor(Colors.White);

                                });

                                int i = 1;

                                foreach (var user in users)
                                {
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{i}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.FirstName}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.MiddleName}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text($"{user.LastName}");
                                    table.Cell().Padding(1, Unit.Millimetre).Text("");
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
            .GeneratePdf(filename);
        }
    }
}
