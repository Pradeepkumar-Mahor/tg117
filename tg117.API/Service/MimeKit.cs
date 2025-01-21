using MailKit.Net.Smtp;
using MimeKit;

namespace tg117.API.Service
{
    public class MimeKit
    {
        public void SendEmailFromMailKit(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Pradeepkumar Mahor", "pradeepmahor47@outlook.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("pradeepmahor47@outlook.com", "ritsrpzyjrnlsnrb");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}