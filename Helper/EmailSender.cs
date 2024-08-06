using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MizeApi.Helper
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message, List<string> cc)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("crm@mize.com.sa"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                if (cc.Count > 0)
                {
                    foreach (var item in cc)
                    {
                        emailToSend.Cc.Add(MailboxAddress.Parse(item));
                    }
                }
                    
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };
                //send email
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                using var emailClient = new MailKit.Net.Smtp.SmtpClient();
                emailClient.LocalDomain = "mize.com.sa";
                emailClient.Connect("smtppro.zoho.com", 465,true);//587//465
                emailClient.Authenticate("crm@mize.com.sa", "sz5JqvwXAVky");
                await emailClient.SendAsync(emailToSend);
                emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
