using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using JetBrains.Annotations;
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
            [UsedImplicitly]
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
