using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Checkout
{
    public static class CheckoutUtils
    {
        public const string Type = "type";
        
        public static string GetAssemblyVersion<T>()
        {
            var containingAssembly = typeof(T).GetTypeInfo().Assembly;

            return containingAssembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?
                .InformationalVersion;
        }

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

        public static T GetEnumFromStringMemberValue<T>(string value)
            where T : struct, IConvertible
        {
            ValidateEnumType<T>();
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                var name = GetEnumMemberValue(enumValue);
                if (value.Equals(name))
                {
                    return enumValue;
                }
            }

            // returns first element by default
            return (T) typeof(T).GetEnumValues().GetValue(0);
        }

        private static void ValidateEnumType<T>()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("T must be an Enumeration type.");
            }
        }

        public static void ValidateParams(string p1, object o1)
        {
            ValidateMultipleParams(new[,] {{p1, o1}});
        }

        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}});
        }

        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2,
            string p3, object o3)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}, {p3, o3}});
        }

        public static void ValidateParams(
            string p1, object o1,
            string p2, object o2,
            string p3, object o3,
            string p4, object o4)
        {
            ValidateMultipleParams(new[,] {{p1, o1}, {p2, o2}, {p3, o3}, {p4, o4}});
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
            if (string.IsNullOrEmpty(content))
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