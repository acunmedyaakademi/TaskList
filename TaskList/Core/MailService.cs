using System.Net;
using System.Net.Mail;

namespace TestApp.Core
{
    public class MailService
    {
        public void SendMail(string mailTo, string message)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onay@ekip2.com");
                mail.To.Add(mailTo);
                mail.Subject = "Test Project by Ekip2";
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp-relay.sendinblue.com"))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = "smtp-relay.sendinblue.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("sahil.emirov1@gmail.com", "pKAJqWkX91C6RcyT");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
