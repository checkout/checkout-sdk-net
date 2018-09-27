namespace Checkout
{
    /// <summary>
    /// Extensions for <see cref="System.String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Trims the provided <paramref="input"/> if it exceeds <paramref="length"/>.
        /// </summary>
        /// <param name="input">The input string to trim.</param>
        /// <param name="length">The input length.</param>
        /// <returns></returns>
        public static string Trim(this string input, int length)
        {
            if (input == null)
                return input;

            return input.Length > length ? input.Substring(0, length) : input;
        }
    }
}