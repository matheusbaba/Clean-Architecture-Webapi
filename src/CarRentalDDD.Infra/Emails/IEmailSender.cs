using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Emails
{
    public interface IEmailSender
    {
        Task SendAsync(Email email);
    }
}
