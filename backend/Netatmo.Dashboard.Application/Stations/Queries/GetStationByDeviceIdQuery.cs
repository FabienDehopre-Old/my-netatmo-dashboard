using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Application.Stations.DTOs;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Application.Stations.Queries
{
    public class GetStationByDeviceIdQuery : IRequest<StationDto>
    {
        public string DeviceId { get; set; }
        public bool ThrowWhenNotFound { get; set; }
    }

    public class GetStationByDeviceIdQueryHandler : IRequestHandler<GetStationByDeviceIdQuery, StationDto>
    {
        private readonly INetatmoDbContext context;

        public GetStationByDeviceIdQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<StationDto> Handle(GetStationByDeviceIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Stations.SingleOrDefaultAsync(s => s.Devices.Select(d => d.Id).Contains(request.DeviceId));
            if (entity == null && request.ThrowWhenNotFound)
            {
                throw new NotFoundException(nameof(Station), request.DeviceId);
            }

            return entity != null ? StationDto.Create(entity) : null;
        }
    }
}
