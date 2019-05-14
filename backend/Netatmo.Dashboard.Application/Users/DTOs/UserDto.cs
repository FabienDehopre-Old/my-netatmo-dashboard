using Netatmo.Dashboard.Domain;
using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Netatmo.Dashboard.Application.Users.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string UpdateJobId { get; set; }
        public FeelLikeAlgo? FeelLike { get; set; }
        public PressureUnit? PressureUnit { get; set; }
        public Unit? Unit { get; set; }
        public WindUnit? WindUnit { get; set; }

        public static Expression<Func<User, UserDto>> Projection
        {
            get
            {
                return u => new UserDto
                {
                    Id = u.Id,
                    Uid = u.Uid,
                    AccessToken = u.AccessToken,
                    ExpiresAt = u.ExpiresAt,
                    RefreshToken = u.RefreshToken,
                    UpdateJobId = u.UpdateJobId,
                    FeelLike = u.FeelLike,
                    PressureUnit = u.PressureUnit,
                    Unit = u.Unit,
                    WindUnit = u.WindUnit
                };
            }
        }

        public static UserDto Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}
