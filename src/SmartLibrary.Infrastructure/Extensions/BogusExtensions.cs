using Bogus;

namespace SmartLibrary.Infrastructure.Extensions
{
    public static class BogusExtensions
    {
        public static string Isbn(this Randomizer randomizer)
        {
            var digits = string.Join("", randomizer.Digits(13));
            return $"{digits[..3]}-{digits[3]}-{digits[4..6]}-{digits[6..12]}-{digits[12]}";
        }
    }
}
