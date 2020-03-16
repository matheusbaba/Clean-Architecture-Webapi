using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Emails
{
    /// <summary>
    /// Send email
    /// </summary>
    public interface IEmailSender
    {
        Task SendAsync(Email email);
    }
}
