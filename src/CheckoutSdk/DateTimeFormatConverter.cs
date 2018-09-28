using Newtonsoft.Json.Converters;

namespace Checkout
{
    /// <summary>
    /// Custom JSON converter for converting dates with a specified ISO format.
    /// </summary>
    public class DateTimeFormatConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Creates a new <see cref="DateTimeFormatConverter"/> with the specified <paramref="format"/>.
        /// </summary>
        /// <param name="format">The ISO format.</param>
        public DateTimeFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}