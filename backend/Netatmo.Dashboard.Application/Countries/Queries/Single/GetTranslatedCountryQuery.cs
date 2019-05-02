using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Netatmo.Dashboard.Application.Countries.Queries.DTOs;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Application.Countries.Queries.Single
{
    public class GetTranslatedCountryQuery : IRequest<TranslatedCountryDto>
    {
        public string Code { get; set; }
        public string Language { get; set; }
    }

    public class GetTranslatedCountryQueryHandler : IRequestHandler<GetTranslatedCountryQuery, TranslatedCountryDto>
    {
        private readonly INetatmoDbContext context;

        public GetTranslatedCountryQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TranslatedCountryDto> Handle(GetTranslatedCountryQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Countries.FindAsync(request.Code);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Code);
            }

            return TranslatedCountryDto.Create(entity, request.Language);
        }
    }
}
