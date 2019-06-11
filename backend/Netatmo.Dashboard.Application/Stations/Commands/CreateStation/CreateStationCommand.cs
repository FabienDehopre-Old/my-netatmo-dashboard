using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Application.Stations.Commands.CreateStation
{
    public class CreateStationCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int Altitude { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Timezone { get; set; }
        public string StaticMap { get; set; }
        public int UserId { get; set; }

        public class Handler : IRequestHandler<CreateStationCommand, int>
        {
            private readonly INetatmoDbContext context;

            public Handler(INetatmoDbContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<int> Handle(CreateStationCommand request, CancellationToken cancellationToken)
            {
                var station = new Station
                {
                    Name = request.Name,
                    Altitude = request.Altitude,
                    City = request.City,
                    CountryCode = request.CountryCode,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Timezone = request.Timezone,
                    StaticMap = request.StaticMap,
                    UserId = request.UserId
                };

                await context.Stations.AddAsync(station, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return station.Id;
            }
        }
    }
}
