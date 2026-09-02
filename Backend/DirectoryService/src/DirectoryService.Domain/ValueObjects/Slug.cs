using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirectoryService.Domain.ValueObjects
{
    public record Slug
    {
        public string Value { get; }

        public Slug(string value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(value);

            if (!Regex.IsMatch(value, @"^[a-z0-9]+(?:-[a-z0-9]+)*$"))
            {
                throw new ArgumentException(
                    "Слаг должен содержать только строчные латинские буквы, цифры и одиночные дефисы.",
                    nameof(value)
                );
            }

            if (value.Length > 100)
                throw new ArgumentException("Слаг превышает 100 символов", nameof(value));

            Value = value;
        }

        public static Slug Empty{get;} = new Slug(string.Empty);
    }
}
