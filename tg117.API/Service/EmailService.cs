using System.Net.Mail;
using System.Net;

namespace tg117.API.Service
{
    public class EmailService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("pradeepmahor47@outlook.com", "Pradeepkumar Mahor");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "ritsrpzyjrnlsnrb";

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}