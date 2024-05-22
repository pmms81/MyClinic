using MimeKit;
using MailKit;
using MyClinic.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
//using System.Net.Mail;

/*
 * Email account:
 * devpmms81@gmail.com
* tt98t436
* -------------------------------
* Mailchimp:
* devpmms81@gmail.com
* $T98t436
* -------------------------------
* https://myaccount.google.com/apppasswords -> To define password for third-party applications
*--------------------------------
*https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/#:~:text=Sending%20emails%20from%20C%23%20using,subject%22%2C%20%22body%22)%3B
*/

namespace MyClinic.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            this._smtpSettings = smtpSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //string mail = "devpmms81@gmail.com", pwd = "otvy zcnt qfku meij";
            
            //var client = new SmtpClient("smtp.gmail.com", 465) //TLS: 587

            /*
            var client = new SmtpClient("smtp.mandrillapp.com")
            {
                Port = 465,
                Host = "smtp.mandrillapp.com", //"smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pwd),
                EnableSsl = true,
                DeliveryMethod= SmtpDeliveryMethod.Network
            };


            return client.SendMailAsync(
                    new MailMessage(
                            from: mail,
                            to: email,
                            subject :subject,
                            body: message
                        )
                    );
            */

            var mailMessage = new MimeMessage();
            //mailMessage.From.Add(new MailboxAddress("devpmms81", "devpmms81@gmail.com"));
            mailMessage.From.Add(new MailboxAddress("devpmms81", _smtpSettings.SenderEmail));
            mailMessage.To.Add(new MailboxAddress("pmms81", "pmms81@gmail.com"));
            mailMessage.Subject = "Hello there";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Hello"
            };

            SmtpClient smtpClient = new SmtpClient();

            //smtpClient.Connect("smtp.gmail.com", 465,true);
            //smtpClient.Authenticate(mail, pwd);
            smtpClient.Connect(_smtpSettings.Server, _smtpSettings.Port, true);
            smtpClient.Authenticate(_smtpSettings.SenderEmail, _smtpSettings.SenderPassword);

            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);

            

            return Task.CompletedTask;


        }

    }
}
