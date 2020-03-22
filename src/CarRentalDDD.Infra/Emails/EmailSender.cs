using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Emails
{
    /// <summary>
    /// Send email
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public Task SendAsync(Email email)
        {
            _logger.LogInformation("Sending email...");
            return Task.Run(() => _logger.LogWarning("EmailSender.SendAsync(email) - not implemented"));
        }
    }
}
