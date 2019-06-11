using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Netatmo.Dashboard.Application.Stations.DTOs
{
    public class StationDto
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

        public static Expression<Func<Station, StationDto>> Projection
        {
            get
            {
                return s => new StationDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Altitude = s.Altitude,
                    City = s.City,
                    CountryCode = s.CountryCode,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    Timezone = s.Timezone,
                    StaticMap = s.StaticMap
                };
            }
        }

        public static StationDto Create(Station station)
        {
            return Projection.Compile().Invoke(station);
        }
    }
}
