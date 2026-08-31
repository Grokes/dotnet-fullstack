using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirectoryService.Domain.ValueObjects
{
    public record Address
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string Office { get; }
        private static readonly Regex NameRegex = new(@"^[\p{L}\s\-']+$", RegexOptions.Compiled);

        public Address(string country, string city, string street, string office)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(country);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(city);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(street);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(office);

            if (country.Length > 100)
                throw new ArgumentException(
                    "Название страны превышает 100 символов.",
                    nameof(country)
                );

            if (city.Length > 100)
                throw new ArgumentException("Название города превышает 100 символов.", nameof(city));

            if (street.Length > 200)
                throw new ArgumentException(
                    "Название улицы превышает 200 символов.",
                    nameof(street)
                );

            if (office.Length > 20)
                throw new ArgumentException("Название офиса превышает 20 символов.", nameof(office));

            if (!NameRegex.IsMatch(country))
                throw new ArgumentException(
                    "Название страны должно содержать только буквы, пробелы, дефисы или апострофы.",
                    nameof(country)
                );

            if (!NameRegex.IsMatch(city))
                throw new ArgumentException(
                    "Название города должно содержать только буквы, пробелы, дефисы или апострофы.",
                    nameof(city)
                );
            
            Country = country;
            City = city;
            Street = street;
            Office = office;
        }
    }
}
