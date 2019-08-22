using System.Diagnostics;
using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendAsync(Email email)
        {
            Debug.Write("EmailSender.SendAsync(email) - not implemented");
            return;
        }
    }
}
