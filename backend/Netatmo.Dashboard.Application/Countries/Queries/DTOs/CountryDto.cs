using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Netatmo.Dashboard.Application.Countries.Queries.DTOs
{
    public class CountryDto
    {
        public string Code { get; set; }
        public string Flag { get; set; }
        public string NameBR { get; set; }
        public string NameDE { get; set; }
        public string NameEN { get; set; }
        public string NameES { get; set; }
        public string NameFA { get; set; }
        public string NameFR { get; set; }
        public string NameHR { get; set; }
        public string NameIT { get; set; }
        public string NameJA { get; set; }
        public string NameNL { get; set; }
        public string NamePT { get; set; }

        public static Expression<Func<Country, CountryDto>> Projection
        {
            get
            {
                return c => new CountryDto
                {
                    Code = c.Code,
                    Flag = c.Flag,
                    NameBR = c.NameBR,
                    NameDE = c.NameDE,
                    NameEN = c.NameEN,
                    NameES = c.NameES,
                    NameFA = c.NameFA,
                    NameFR = c.NameFR,
                    NameHR = c.NameHR,
                    NameIT = c.NameIT,
                    NameJA = c.NameJA,
                    NameNL = c.NameNL,
                    NamePT = c.NamePT
                };
            }
        }

        public static CountryDto Create(Country country)
        {
            return Projection.Compile().Invoke(country);
        }
    }
}
