using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Application.Stations.Commands.UpdateStation
{
    public class UpdateStationCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Altitude { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Timezone { get; set; }
        public string StaticMap { get; set; }

        public class Handler : IRequestHandler<UpdateStationCommand, Unit>
        {
            private readonly INetatmoDbContext context;

            public Handler(INetatmoDbContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<Unit> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Stations.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Station), request.Id);
                }

                entity.Name = request.Name;
                entity.Altitude = request.Altitude;
                entity.City = request.City;
                entity.CountryCode = request.CountryCode;
                entity.Latitude = request.Latitude;
                entity.Longitude = request.Longitude;
                entity.Timezone = request.Timezone;
                entity.StaticMap = request.StaticMap;

                await context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
