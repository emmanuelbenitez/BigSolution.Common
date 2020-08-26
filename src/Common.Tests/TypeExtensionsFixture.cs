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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace BigSolution
{
    public class TypeExtensionsFixture
    {
        [Theory]
        [MemberData(nameof(InvalidParametersForImplements))]
        public void ImplementsFailed(Type type, Type interfaceType)
        {
            Action action = () => type.Implements(interfaceType);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ImplementsGenericInterfaceFailed()
        {
            typeof(string).Implements(typeof(IGenericInterface<>)).Should().BeFalse();
        }

        [Fact]
        public void ImplementsGenericInterfaceSucceeds()
        {
            typeof(GenericClass<string>).Implements(typeof(IGenericInterface<>)).Should().BeTrue();
        }

        [Fact]
        public void ImplementsGenericSucceeds()
        {
            typeof(Interface).Implements<IInterface>().Should().BeTrue();
        }

        [Fact]
        public void ImplementsInterfaceSucceeds()
        {
            typeof(Interface).Implements(typeof(IInterface)).Should().BeTrue();
        }

        [Fact]
        public void IsInterfaceFailed()
        {
            typeof(object).Is<IInterface>().Should().BeFalse();
        }

        [Fact]
        public void IsInterfaceSucceeds()
        {
            typeof(Interface).Is<IInterface>().Should().BeTrue();
        }

        public static IEnumerable<object[]> InvalidParametersForImplements
        {
            get
            {
                yield return new object[] { null, typeof(IInterface) };
                yield return new object[] { typeof(string), null };
                yield return new object[] { typeof(string), typeof(int) };
            }
        }

        private interface IInterface { }

        private class Interface : IInterface { }

        [SuppressMessage("ReSharper", "UnusedTypeParameter")]
        private interface IGenericInterface<T> { }

        private class GenericClass<T> : IGenericInterface<T> { }
    }
}
