using System.Linq;
using BigSolution;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
        public static bool Is<T>(this Type type)
        {
            Requires.NotNull(type, nameof(type));
            return typeof(T).IsAssignableFrom(type);
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            Requires.NotNull(type, nameof(type));
            Requires.NotNull(interfaceType, nameof(interfaceType));
            Requires.IsValid(interfaceType.IsInterface, nameof(interfaceType), "The type is not an interface");

            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType || x == interfaceType);
        }

        public static bool Implements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);

            return type.Implements(interfaceType);
        }
    }
}
