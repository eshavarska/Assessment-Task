using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class MailArguments
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SmtpHost { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

    }
}
