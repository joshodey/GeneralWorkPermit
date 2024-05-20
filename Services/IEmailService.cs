using GeneralWorkPermit.EmailService;

namespace GeneralWorkPermit.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
