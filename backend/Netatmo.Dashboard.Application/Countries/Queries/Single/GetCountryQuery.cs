using MediatR;
using Netatmo.Dashboard.Application.Countries.DTOs;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Countries.Queries.Single
{
    public class GetCountryQuery : IRequest<CountryDto>
    {
        public string Code { get; set; }
    }

    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDto>
    {
        private readonly INetatmoDbContext context;

        public GetCountryQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CountryDto> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Countries.FindAsync(request.Code);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Code);
            }

            return CountryDto.Create(entity);
        }
    }
}
