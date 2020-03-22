using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentalContext _rentalContext;
        private readonly IMediator _mediator;

        public void Dispose()
        {
            _rentalContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public UnitOfWork(RentalContext rentalContext, IMediator mediator)
        {
            _rentalContext = rentalContext;
            _mediator = mediator;
        }

        /// <summary>
        /// Save changes in DbContext and dipatch all entity domain events
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int i = await this._rentalContext.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(_rentalContext);
            return i;
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}
