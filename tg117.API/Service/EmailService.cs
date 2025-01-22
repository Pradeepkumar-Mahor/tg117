using System.Net;
using System.Net.Mail;

namespace tg117.API.Service
{
    public class EmailService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            MailAddress fromAddress = new("pradeepmahor47@outlook.com", "Pradeepkumar Mahor");
            MailAddress toAddress = new(toEmail);
            const string fromPassword = "ritsrpzyjrnlsnrb";

            SmtpClient smtp = new()
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (MailMessage message = new(fromAddress, toAddress)
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