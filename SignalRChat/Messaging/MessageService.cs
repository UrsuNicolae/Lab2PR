using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace SignalRChat.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly Mail mail;
        public MessageService(IOptions<Mail> mail)
        {
            this.mail = mail.Value;
        }

        public async Task SendEmailAsync(MessageOptions message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(message.fromDisplayName, message.fromEmailAddress));
            email.To.Add(new MailboxAddress(message.toEamilAddress));
            email.Subject = message.subjcet;
            email.Body = new TextPart(TextFormat.Html) { Text = message.message };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback =
                    (sender, certificate, certchainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.ConnectAsync(mail.Server, mail.Port, false).ConfigureAwait(false);
                await client.AuthenticateAsync(mail.UserName, mail.Password).ConfigureAwait(false);
                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
        }
    }
}
