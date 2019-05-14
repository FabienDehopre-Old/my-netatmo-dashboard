using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Netatmo.Dashboard.Application.Countries.DTOs
{
    public class TranslatedCountryDto
    {
        public string Code { get; set; }
        public string Flag { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Country, TranslatedCountryDto>> Projection(string language)
        {
            var param = Expression.Parameter(typeof(Country), "c");
            var newDto = Expression.New(typeof(TranslatedCountryDto));
            var bindings = new[] { "Code", "Flag", $"Name{language.ToUpper()}" }.Select(
                o =>
                {
                    var prop = typeof(Country).GetProperty(o);
                    return Expression.Bind(prop, Expression.Property(param, prop));
                });
            var init = Expression.MemberInit(newDto, bindings);
            return Expression.Lambda<Func<Country, TranslatedCountryDto>>(init, param);
        }

        public static TranslatedCountryDto Create(Country country, string language)
        {
            return Projection(language).Compile().Invoke(country);
        }
    }
}