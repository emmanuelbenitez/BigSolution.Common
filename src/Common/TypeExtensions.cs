#region Copyright & License

// Copyright © 2020 - 2020 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Linq;
using BigSolution;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
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

        public static bool Is<T>(this Type type)
        {
            Requires.NotNull(type, nameof(type));
            return typeof(T).IsAssignableFrom(type);
        }
    }
}
