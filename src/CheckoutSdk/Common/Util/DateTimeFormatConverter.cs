using Newtonsoft.Json.Converters;

namespace Checkout.Common.Util
{
    public sealed class DateTimeFormatConverter : IsoDateTimeConverter
    {
        public DateTimeFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}