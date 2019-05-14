using MediatR;
using Microsoft.EntityFrameworkCore;
using Netatmo.Dashboard.Application.Exceptions;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Domain;
using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string UpdateJobId { get; set; }
        public FeelLikeAlgo? FeelLike { get; set; }
        public PressureUnit? PressureUnit { get; set; }
        public Domain.Unit? Unit { get; set; }
        public WindUnit? WindUnit { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, MediatR.Unit>
        {
            private readonly INetatmoDbContext context;

            public Handler(INetatmoDbContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<MediatR.Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var entity = await context.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(User), request.Id);
                }

                entity.AccessToken = request.AccessToken;
                entity.ExpiresAt = request.ExpiresAt;
                entity.RefreshToken = request.RefreshToken;
                entity.UpdateJobId = request.UpdateJobId;
                entity.FeelLike = request.FeelLike;
                entity.PressureUnit = request.PressureUnit;
                entity.Unit = request.Unit;
                entity.WindUnit = request.WindUnit;

                await context.SaveChangesAsync(cancellationToken);
                return MediatR.Unit.Value;
            }
        }
    }
}
