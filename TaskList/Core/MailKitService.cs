using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TestApp.Core
{
    public class MailKitService
    {
        public bool SendMailPassword(string mailTo, string code)
        {
            var emailMessage = new MimeMessage();
            //attach =  attach.Replace("\\", "//");
            emailMessage.From.Add(new MailboxAddress("TaskListApp(HACKLENEMEZ)", "onay@ekip2.com"));
            emailMessage.To.Add(new MailboxAddress("Kullanici", mailTo));
            //emailMessage.Cc.Add(new MailboxAddress("CC you", "sahil.emirov2@gmail.com"));
            //emailMessage.Bcc.Add(new MailboxAddress("BCC you", "beststore4world@gmail.com"));
            emailMessage.Subject = "Test Project by TaskListApp";
            var builder = new BodyBuilder();
            string template = MailTemplate.MailPassword;
            code = template.Replace("{password}", code).Replace("{Code}", code);
            builder.HtmlBody = code;
            //if (attach != null)
            //builder.Attachments.Add(attach);
            emailMessage.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp-relay.sendinblue.com",587, SecureSocketOptions.StartTls);
                smtp.Authenticate("sahil.emirov1@gmail.com", "pKAJqWkX91C6RcyT");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }

            return true;
        }

        public void SendMailList(List<string> mailTo, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("BlogApp", "onay@ekip2.com"));
            for (int i = 0; i < mailTo.Count; i++)
            {
                emailMessage.To.Add(new MailboxAddress("Kullanici", mailTo[i]));
            }
            //emailMessage.Cc.Add(new MailboxAddress("CC you", "sahil.emirov2@gmail.com"));
            //emailMessage.Bcc.Add(new MailboxAddress("BCC you", "beststore4world@gmail.com"));
            emailMessage.Subject = "Test Project by Ekip2";
            var builder = new BodyBuilder();
            builder.HtmlBody = message;
            emailMessage.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("sahil.emirov1@gmail.com", "pKAJqWkX91C6RcyT");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }

        }
    }
}
