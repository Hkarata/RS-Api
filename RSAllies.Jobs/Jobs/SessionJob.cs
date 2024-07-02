using MailKit.Net.Smtp;
using MimeKit;
using RSAllies.Jobs.Services;

namespace RSAllies.Jobs.Jobs
{
    internal class SessionJob(DatabaseService database)
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);


        public async Task ExecuteAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var venue = await database.GetVenueDetails(sessionId);
            var users = await database.GetUserDetails(sessionId);

            // Cancel other bookings
            await database.CancelUnconfirmedBookings(sessionId);

            if (users.Count == 0)
            {
                return;
            }
            else
            {
                DocumentService.GenerateSessionPdf(sessionId, venue, users);



                // Send email
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("mail.privateemail.com", 465, true);
                    smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

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

                    await _semaphore.WaitAsync();
                    await smtpClient.SendAsync(message);
                    _semaphore.Release();
                }
            }

        }
    }
}
