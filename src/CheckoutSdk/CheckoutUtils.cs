using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Checkout
{
    /// <summary>
    /// Helper methods for assembly metadata, enum member values and parameter validation.
    /// </summary>
    public static class CheckoutUtils
    {
        /// <summary>
        /// The JSON property name "type".
        /// </summary>
        public const string Type = "type";
        /// <summary>
        /// The JSON property name "control_type".
        /// </summary>
        public const string ControlType = "control_type";
        /// <summary>
        /// The JSON property name "frequency".
        /// </summary>
        public const string Frequency = "frequency";

        /// <summary>
        /// Returns the informational version of the assembly that contains the given type.
        /// </summary>
        /// <typeparam name="T">A type in the assembly to inspect.</typeparam>
        /// <returns>The informational version, or null when the assembly has none.</returns>
        public static string GetAssemblyVersion<T>()
        {
            var containingAssembly = typeof(T).GetTypeInfo().Assembly;

            return containingAssembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?
                .InformationalVersion;
        }

        /// <summary>
        /// Returns the EnumMember value of an enum member.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum member.</param>
        /// <returns>The EnumMember value, or null when the member has no EnumMember attribute.</returns>
        /// <exception cref="InvalidOperationException">T is not an enum type.</exception>
        public static string GetEnumMemberValue<T>(T value)
            where T : struct, IConvertible
        {
            ValidateEnumType<T>();
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        /// <summary>
        /// Returns the enum member whose EnumMember value equals the given string.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The EnumMember value to look up, compared with ordinal case sensitive equality.</param>
        /// <returns>The matching enum member, or null when value is null or no member matches.</returns>
        /// <exception cref="InvalidOperationException">T is not an enum type.</exception>
        public static T? GetEnumFromStringMemberValue<T>(string value)
            where T : struct, IConvertible
        {
            ValidateEnumType<T>();
            if (value == null)
            {
                return null;
            }

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                var name = GetEnumMemberValue(enumValue);
                if (value.Equals(name))
                {
                    return enumValue;
                }
            }

            return null;
        }

        private static void ValidateEnumType<T>()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an Enumeration type.");
            }
        }

        // Credentials travel with every request to these URIs, so plain http is only accepted for
        // loopback hosts (local mocks and tests). The message names the parameter, never the URI.
        internal static void ValidateHttpsUri(string name, Uri uri)
        {
            if (uri == null)
            {
                return;
            }

            if (!uri.IsAbsoluteUri)
            {
                throw new CheckoutArgumentException($"{name} must be an absolute https URI");
            }

            var isHttps = uri.Scheme == Uri.UriSchemeHttps;
            var isLoopbackHttp = uri.Scheme == Uri.UriSchemeHttp && uri.IsLoopback;
            if (!isHttps && !isLoopbackHttp)
            {
                throw new CheckoutArgumentException($"{name} must use https");
            }
        }

        internal const int MaxTokenLength = 4096;

        private const string TokenPunctuation = "-._~+/=";

        // A value sent as the whole Authorization header (session secret, Custom platform type):
        // RFC 7235 token68 characters only, bounded length. The message never echoes the value.
        internal static void ValidateToken(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new CheckoutArgumentException($"{name} cannot be empty");
            }

            if (value.Length > MaxTokenLength)
            {
                throw new CheckoutArgumentException($"{name} exceeds the maximum length");
            }

            foreach (var c in value)
            {
                if (!IsTokenCharacter(c))
                {
                    throw new CheckoutArgumentException($"{name} contains characters that are not allowed");
                }
            }
        }

        private static bool IsTokenCharacter(char c)
        {
            if (c > 0x7F)
            {
                return false;
            }

            return char.IsLetterOrDigit(c) || TokenPunctuation.IndexOf(c) >= 0;
        }

        // Any outgoing header value: visible ASCII and space only, so no CR/LF, tab or non-ASCII
        // reaches the wire on any target framework. The message names the header, never the value.
        internal static void ValidateHeaderValue(string name, string value)
        {
            foreach (var c in value)
            {
                if (c < 0x20 || c > 0x7E)
                {
                    throw new CheckoutArgumentException($"{name} header value contains characters that are not allowed");
                }
            }
        }

        /// <summary>
        /// Validates that each string parameter is not null or blank and every other parameter is not null.
        /// </summary>
        /// <param name="p1">The name of parameter 1, used in the exception message.</param>
        /// <param name="o1">The value of parameter 1.</param>
        /// <exception cref="CheckoutArgumentException">A name is null or empty, a string value is null or blank, or another value is null.</exception>
        public static void ValidateParams(string p1, object o1)
        {
            ValidateMultipleParams(new[,] {{p1, o1}});
        }

        /// <summary>
        /// Validates that each string parameter is not null or blank and every other parameter is not null.
        /// </summary>
        /// <param name="p1">The name of parameter 1, used in the exception message.</param>
        /// <param name="o1">The value of parameter 1.</param>
        /// <param name="p2">The name of parameter 2, used in the exception message.</param>
        /// <param name="o2">The value of parameter 2.</param>
        /// <exception cref="CheckoutArgumentException">A name is null or empty, a string value is null or blank, or another value is null.</exception>
        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}});
        }

        /// <summary>
        /// Validates that each string parameter is not null or blank and every other parameter is not null.
        /// </summary>
        /// <param name="p1">The name of parameter 1, used in the exception message.</param>
        /// <param name="o1">The value of parameter 1.</param>
        /// <param name="p2">The name of parameter 2, used in the exception message.</param>
        /// <param name="o2">The value of parameter 2.</param>
        /// <param name="p3">The name of parameter 3, used in the exception message.</param>
        /// <param name="o3">The value of parameter 3.</param>
        /// <exception cref="CheckoutArgumentException">A name is null or empty, a string value is null or blank, or another value is null.</exception>
        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2,
            string p3, object o3)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}, {p3, o3}});
        }

        /// <summary>
        /// Validates that each string parameter is not null or blank and every other parameter is not null.
        /// </summary>
        /// <param name="p1">The name of parameter 1, used in the exception message.</param>
        /// <param name="o1">The value of parameter 1.</param>
        /// <param name="p2">The name of parameter 2, used in the exception message.</param>
        /// <param name="o2">The value of parameter 2.</param>
        /// <param name="p3">The name of parameter 3, used in the exception message.</param>
        /// <param name="o3">The value of parameter 3.</param>
        /// <param name="p4">The name of parameter 4, used in the exception message.</param>
        /// <param name="o4">The value of parameter 4.</param>
        /// <exception cref="CheckoutArgumentException">A name is null or empty, a string value is null or blank, or another value is null.</exception>
        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2,
            string p3, object o3,
            string p4, object o4)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}, {p3, o3}, {p4, o4}});
        }

        /// <summary>
        /// Validates that each string parameter is not null or blank and every other parameter is not null.
        /// </summary>
        /// <param name="p1">The name of parameter 1, used in the exception message.</param>
        /// <param name="o1">The value of parameter 1.</param>
        /// <param name="p2">The name of parameter 2, used in the exception message.</param>
        /// <param name="o2">The value of parameter 2.</param>
        /// <param name="p3">The name of parameter 3, used in the exception message.</param>
        /// <param name="o3">The value of parameter 3.</param>
        /// <param name="p4">The name of parameter 4, used in the exception message.</param>
        /// <param name="o4">The value of parameter 4.</param>
        /// <param name="p5">The name of parameter 5, used in the exception message.</param>
        /// <param name="o5">The value of parameter 5.</param>
        /// <exception cref="CheckoutArgumentException">A name is null or empty, a string value is null or blank, or another value is null.</exception>
        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2,
            string p3, object o3,
            string p4, object o4,
            string p5, object o5)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}, {p3, o3}, {p4, o4}, {p5, o5}});
        }

        private static void ValidateMultipleParams(object[,] parameters)
        {
            if (parameters.Length == 0)
            {
                return;
            }

            for (var i = 0; i < parameters.GetLength(0); i++)
            {
                var property = parameters.GetValue(i, 0);
                if (!(property is string prop) || string.IsNullOrEmpty(prop))
                {
                    throw CheckoutArgumentException.WithMessage("invalid validation key");
                }

                var value = parameters.GetValue(i, 1);
                if (value is string s)
                {
                    RequiresNonBlank(prop, s);
                    continue;
                }

                RequiresNonNull(prop, value);
            }
        }

        private static void RequiresNonBlank(string property, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw CheckoutArgumentException.WithMessage($"{property} cannot be blank");
            }
        }

        private static void RequiresNonNull(string property, object obj)
        {
            if (obj is null)
            {
                throw CheckoutArgumentException.WithMessage($"{property} cannot be null");
            }
        }
    }
}