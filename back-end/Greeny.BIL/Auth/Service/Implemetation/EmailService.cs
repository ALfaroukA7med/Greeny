
using Greeny.BLL.Auth.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
//using System.Net.Mail;
public class EmailService : IEmailService
{


    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        using (var Client = new SmtpClient())
        {
            await Client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await Client.AuthenticateAsync("ahmed.mohamed.alslahy@gmail.com", "hvzz igxd shat przk");

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body,
            };

            var message = new MimeMessage
            {
                Body = bodyBuilder.ToMessageBody()
            };
            message.From.Add(MailboxAddress.Parse("ahmed.mohamed.alslahy@gmail.com"));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            await Client.SendAsync(message);
            await Client.DisconnectAsync(true);


        }
        return true;
    }



}