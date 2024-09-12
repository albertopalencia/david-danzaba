using RealState.Application.Ports;
using RealState.Domain.Reservations.Service;
using MediatR;
using RealState.Application.Properties.Command;
using RealState.Application.Properties.Command.Factory;

namespace RealState.Application.Reservations.Command
{
    internal class InsertReservationHandler(PropertyFactory propertyFactory, InsertReservationService reservationService
        , IUnitOfWork unitOfWork) : IRequestHandler<InsertPropertyCommand, Guid>
    {
        public async Task<Guid> Handle(InsertPropertyCommand request, CancellationToken cancellationToken)
        {
            var reservation = await propertyFactory.CreateAsync(request);
            var reservationId = await reservationService.ExecuteAsync(reservation);
            await unitOfWork.SaveAsync();
            return reservationId;
        }
    }
}
