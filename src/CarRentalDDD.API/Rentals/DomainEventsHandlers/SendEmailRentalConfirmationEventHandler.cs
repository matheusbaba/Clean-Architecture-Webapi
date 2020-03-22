using CarRentalDDD.Domain.Models.Rentals.DomainEvents;
using CarRentalDDD.Infra.Emails;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Rentals.DomainEventsHandlers
{
    /// <summary>
    /// Send email confirmation for every rental created     
    /// </summary>
    public class SendEmailRentalConfirmationEventHandler : INotificationHandler<RentalCreatedDomainEvent>
    {
        private IEmailSender _emailSender;
        public SendEmailRentalConfirmationEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        
        public async Task Handle(RentalCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var email = new Email(
                from: "from@email.com",
                to: notification.Rental.Customer.Email.ToString(),
                subject: "Car Rental Confirmation",
                content: "some content"
                );
            await _emailSender.SendAsync(email).ConfigureAwait(false);            
        }
    }
}
