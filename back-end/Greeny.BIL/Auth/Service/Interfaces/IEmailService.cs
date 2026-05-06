

namespace Greeny.BLL.Auth.Service.Interfaces
{
    public interface IEmailService
    {
        public interface IEmailSender
        {
            Task SendEmailAsync(string to, string subject, string body);
        }

    }
}
