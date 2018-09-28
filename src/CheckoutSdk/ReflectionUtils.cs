using System.Linq;
using System.Reflection;

namespace Checkout
{
    internal class ReflectionUtils
    {
        /// <summary>
        /// Returns the informtational version of the assembly containing the specified type.
        /// </summary>
        public static string GetAssemblyVersion<T>()
        {
            var containingAssembly = typeof(T).GetTypeInfo().Assembly;
            
            return containingAssembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?
                .InformationalVersion;
        }
    }
}