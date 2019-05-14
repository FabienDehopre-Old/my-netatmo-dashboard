using MediatR;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Countries.DTOs;
using Netatmo.Dashboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Countries.Queries.List
{
    public class GetCountriesQuery : IRequest<List<CountryDto>>
    {
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryDto>>
    {
        private readonly INetatmoDbContext context;

        public GetCountriesQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return context.Countries
                .Select(CountryDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}
