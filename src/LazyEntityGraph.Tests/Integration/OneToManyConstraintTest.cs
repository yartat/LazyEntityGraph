using FluentAssertions;
using LazyEntityGraph.Core;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using LazyEntityGraph.Core.Constraints;
using Xunit;

namespace LazyEntityGraph.Tests.Integration
{
    public class OneToManyConstraintTest
    {
        [Fact]
        public void SetsInversePropertyOnGeneratedCollection()
        {
            // arrange
            var fixture = IntegrationTest.GetFixture(new OneToManyPropertyConstraint<Foo, Bar>(f => f.Bars, b => b.Foo));
            var foo = fixture.Create<Foo>();

            // act
            var bars = foo.Bars;

            // assert
            foreach (var bar in bars)
                bar.Foo.Should().BeSameAs(foo);
        }

        [Fact]
        public void SetsInversePropertyOnExternalCollection()
        {
            // arrange
            var fixture = IntegrationTest.GetFixture(new OneToManyPropertyConstraint<Foo, Bar>(f => f.Bars, b => b.Foo));
            var foo = fixture.Create<Foo>();

            // act
            foo.Bars = fixture.Create<ICollection<Bar>>();

            // assert
            foreach (var bar in foo.Bars)
                bar.Foo.Should().BeSameAs(foo);
        }

        [Fact]
        public void SetsInversePropertyOnPOCOAddedToGeneratedCollection()
        {
            // arrange
            var fixture = IntegrationTest.GetFixture(new OneToManyPropertyConstraint<Foo, Bar>(f => f.Bars, b => b.Foo));
            var foo = fixture.Create<Foo>();
            var bar = new Bar();

            // act
            foo.Bars.Add(bar);

            // assert
            bar.Foo.Should().BeSameAs(foo);
        }

        [Fact]
        public void SetsInversePropertyOnProxyAddedToGeneratedCollection()
        {
            // arrange
            var fixture = IntegrationTest.GetFixture(new OneToManyPropertyConstraint<Foo, Bar>(f => f.Bars, b => b.Foo));
            var foo = fixture.Create<Foo>();
            var bar = fixture.Create<Bar>();

            // act
            foo.Bars.Add(bar);

            // assert
            bar.Foo.Should().BeSameAs(foo);
        }
    }
}