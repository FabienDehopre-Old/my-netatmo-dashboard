using Netatmo.Dashboard.Domain.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Netatmo.Dashboard.Persistence
{
    public class NetatmoInitializer
    {
        public static void Initialize(NetatmoDbContext context)
        {
            var initializer = new NetatmoInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(NetatmoDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Countries.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
            SeedCountries(context);
        }

        public void SeedUsers(NetatmoDbContext context)
        {
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Uid = "auth0|5c3369d9b171c101904570ca",
                    AccessToken = "56102b6fc6aa42f174e5d484|a2a52b7b24acfacf1718f69bbf226620",
                    ExpiresAt = DateTimeOffset.FromUnixTimeMilliseconds(1550239201963).Date,
                    RefreshToken = "56102b6fc6aa42f174e5d484|73d5b91c1cb4b021fddf91634be0a598"
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        public void SeedCountries(NetatmoDbContext context)
        {
            var countries = GetAllCountries().GetAwaiter().GetResult();
            context.Countries.AddRange(countries);
            context.SaveChanges();
        }

        private async Task<Country[]> GetAllCountries()
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync("https://restcountries.eu/rest/v2/all?fields=alpha2Code;name;translations;flag"))
                {
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsAsync<RestCountry[]>();
                    return data.Select(x => new Country
                    {
                        Code = x.alpha2Code,
                        Flag = x.flag,
                        NameEN = x.name,
                        NameBR = x.translations.br,
                        NamePT = x.translations.pt,
                        NameNL = x.translations.nl,
                        NameHR = x.translations.hr,
                        NameFA = x.translations.fa,
                        NameDE = x.translations.de,
                        NameES = x.translations.es,
                        NameFR = x.translations.fr,
                        NameJA = x.translations.ja,
                        NameIT = x.translations.it
                    }).ToArray();
                }
            }
        }

        class RestCountry
        {
            public string alpha2Code { get; set; }
            public string flag { get; set; }
            public string name { get; set; }
            public Translations translations { get; set; }
        }

        class Translations
        {
            public string br { get; set; }
            public string pt { get; set; }
            public string nl { get; set; }
            public string hr { get; set; }
            public string fa { get; set; }
            public string de { get; set; }
            public string es { get; set; }
            public string fr { get; set; }
            public string ja { get; set; }
            public string it { get; set; }
        }
    }
}
