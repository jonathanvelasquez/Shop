namespace Shop.Web.Helpers
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Configuration;
    using MimeKit;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration configuration;

        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendMail(string to, string subject, string body)
        {
            string from = this.configuration["Mail:From"];
            string smtp = this.configuration["Mail:Smtp"];
            string port = this.configuration["Mail:Port"];
            string password = this.configuration["Mail:Password"];

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect(smtp, int.Parse(port), false);
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
            }

            
        }
    }

}
