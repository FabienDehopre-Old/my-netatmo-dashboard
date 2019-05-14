using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Application.Users.DTOs;
using Netatmo.Dashboard.Domain.Entities;

namespace Netatmo.Dashboard.Application.Users.Queries
{
    public class GetUserByUidQuery : IRequest<UserDto>
    {
        public string Uid { get; set; }
    }

    public class GetUserByUidQueryHandler : IRequestHandler<GetUserByUidQuery, UserDto>
    {
        private readonly INetatmoDbContext context;

        public GetUserByUidQueryHandler(INetatmoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDto> Handle(GetUserByUidQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Users.SingleOrDefaultAsync(u => u.Uid == request.Uid, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Uid);
            }

            return UserDto.Create(entity);
        }
    }
}
