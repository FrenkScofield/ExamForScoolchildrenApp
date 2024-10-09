using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks.Dataflow;

namespace ExamForScoolchildrenApp.Aplication.Services.EmailService
{
    public class Emailsender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _username;
        private readonly string _password;
        private readonly BufferBlock<MimeMessage> mailMessages;

        public Emailsender(string host, int port, bool ssl, string username, string password)
        {
            _host = host;
            _port = port;
            _ssl = ssl;
            _username = username;
            _password = password;
            this.mailMessages = new BufferBlock<MimeMessage>();
        }

        [Obsolete]
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(email));
            message.From.Add(new MailboxAddress("PROSYS", "kamranaa@code.edu.az"));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(_host, _port, _ssl);
                smtp.Authenticate(_username, _password);
                await smtp.SendAsync(message);
                smtp.Disconnect(true);
            }
            await this.mailMessages.SendAsync(message);
        }

    }
}
