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
    public class GetTranslatedCountriesQuery : IRequest<List<TranslatedCountryDto>>
    {
        public string Language { get; set; }
    }

    public class GetTranslatedCountriesQueryHandler : IRequestHandler<GetTranslatedCountriesQuery, List<TranslatedCountryDto>>
    {
        private readonly INetatmoDbContext context;

        public GetTranslatedCountriesQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<TranslatedCountryDto>> Handle(GetTranslatedCountriesQuery request, CancellationToken cancellationToken)
        {
            return context.Countries
                .Select(TranslatedCountryDto.Projection(request.Language))
                .ToListAsync(cancellationToken);
        }
    }
}
