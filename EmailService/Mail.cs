using AegisImplicitMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmailService
{
    public class Mail
    {
        public static void SendEMail(MailArguments mailArgs, List<MimeAttachment> attachments, bool isSsl, Dictionary<string, string> headers)
        {

            var networkCredential = new NetworkCredential
            {
                Password = mailArgs.Password,
                UserName = mailArgs.MailFrom
            };

            var mailMsg = new MimeMailMessage
            {
                Body = mailArgs.Message,
                Subject = mailArgs.Subject,
                IsBodyHtml = true // This indicates that message body contains the HTML part as well.
            };
            mailMsg.To.Add(mailArgs.MailTo);

            if (headers.IsNotNullOrEmpty())
            {
                foreach (var header in headers)
                {
                    mailMsg.Headers.Add(header.Key, header.Value);
                }
            }

            if (attachments.IsNotNull())
            {
                foreach (var attachment in attachments)
                {
                    if (attachment.IsNotNull())
                    {
                        mailMsg.Attachments.Add(attachment);
                    }
                }
            }
            
            mailMsg.From = new MimeMailAddress(mailArgs.MailFrom);


            var mailer = new MimeMailer(mailArgs.SmtpHost, Convert.ToInt32(mailArgs.Port));
            mailer.User = mailArgs.MailFrom;
            mailer.Password = mailArgs.Password;
            mailer.SslType = SslMode.Ssl;
            mailer.AuthenticationMode = AuthenticationType.Base64;

            mailer.SendMailAsync(mailMsg);
        }

    }
}
