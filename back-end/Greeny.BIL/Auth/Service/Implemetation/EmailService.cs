
using Greeny.BLL.Auth.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.IO;
using Microsoft.AspNetCore.Hosting;

public class EmailService : IEmailService
{
    //private readonly IWebHostEnvironment _hostingEnvironment;

    //public EmailService(IWebHostEnvironment hostingEnvironment)
    //{
    //    _hostingEnvironment = hostingEnvironment;
    //}

    //public async Task<string> GetEmailTemplateAsync()
    //{
    //    var path = Path.Combine(_hostingEnvironment.WebRootPath, "templates", "email_confirmation.html");
    //    var template = await File.ReadAllTextAsync(path);
    //    return template;
    //}
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse("ahmed.mohamed.alslahy@gmail.com"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        // لو HTML
        email.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync("ahmed.mohamed.alslahy@gmail.com", "hvzz igxd shat przk");

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}