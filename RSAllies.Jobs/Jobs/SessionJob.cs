using MailKit.Net.Smtp;
using MimeKit;
using RSAllies.Jobs.Services;

namespace RSAllies.Jobs.Jobs
{
    internal class SessionJob(DatabaseService database, SmtpClient smtpClient)
    {
        public async Task ExecuteAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var venue = await database.GetVenueDetails(sessionId, cancellationToken);
            var users = await database.GetUserDetails(sessionId, cancellationToken);

            DocumentService.GenerateSessionPdf(sessionId, venue, users);

            // Send email

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DoNotReply", "donotreply@roadsafetyallies.me"));
            message.To.Add(new MailboxAddress("Manager", venue.VenueAddress));
            message.Subject = "Session Attendance List";
            var body = new TextPart("plain")
            {
                Text = $"Dear Manager, please find attached the attendance list for the session at {venue.VenueName} in {venue.District}, {venue.Region} " +
                       $"on {venue.Date:dd/MM/yyyy}."
            };

            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", $"{sessionId}.pdf");
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(File.OpenRead(pdfPath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "SessionAttendance.pdf"
            };

            message.Body = new Multipart("mixed")
            {
                body,
                attachment
            };

            await smtpClient.SendAsync(message, cancellationToken);

        }
    }
}
