﻿using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class DimensionsTests
    {
        [Test]
        public void Empty_WhenCalled_ShouldNotBeNull()
        {
            var dimensions = Dimensions.Empty;

            dimensions.Should().NotBeNull();
        }

        [Test]
        public void Empty_WhenCalled_ShouldHaveAllZeros()
        {
            var dimensions = Dimensions.Empty;

            dimensions.ToString().Should().Be("0");
            dimensions.X.Should().Be(0);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }

        [Test]
        public void New_OneDimension_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();

            var dimensions = new Dimensions(x);

            dimensions.ToString().Should().Be(x.ToString());
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }

        [Test]
        public void New_OneDimensionZero_ShouldHaveExpectedValues()
        {
            var dimensions = new Dimensions(0);

            dimensions.ToString().Should().Be(0.ToString());
            dimensions.X.Should().Be(0);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }

        [Test]
        public void New_TwoDimensions_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();

            var dimensions = new Dimensions(x, y);

            dimensions.ToString().Should().Be($"{x} {y}");
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(y);
            dimensions.Z.Should().Be(0);
        }

        [Test]
        public void New_TwoDimensionsXIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var y = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(0, y)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ThreeDimensions_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();

            var dimensions = new Dimensions(x, y, z);

            dimensions.ToString().Should().Be($"{x} {y} {z}");
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(y);
            dimensions.Z.Should().Be(z);
        }

        [Test]
        public void New_ThreeDimensionsYIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(x, 0, z)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ThreeDimensionsXIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var y = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(0, y, z)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void AreEmpty_Empty_ShouldBeTrue()
        {
            var dimension = Dimensions.Empty;

            dimension.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void AreEmpty_NotEmpty_ShouldBeFalse()
        {
            var dimension = new Dimensions(12);

            dimension.IsEmpty.Should().BeFalse();
        }

        [Test]
        public void AreMultiDimensional_MultiDimensional_ShouldBeTure()
        {
            var dimension = new Dimensions(1, 2);

            dimension.IsMultiDimensional.Should().BeTrue();
        }

        [Test]
        public void AreMultiDimensional_SingleDimensional_ShouldBeFalse()
        {
            var dimension = new Dimensions(1);

            dimension.IsMultiDimensional.Should().BeFalse();
        }
        
        [Test]
        public void DegreesOfFreedom_EmptyDimensions_ShouldBeOne()
        {
            var dimensions = Dimensions.Empty;

            dimensions.Rank.Should().Be(0);
        }

        [Test]
        public void DegreesOfFreedom_OneDimensions_ShouldBeOne()
        {
            var dimensions = new Dimensions(10);

            dimensions.Rank.Should().Be(1);
        }
        
        [Test]
        public void DegreesOfFreedom_TwoDimensions_ShouldBeTwo()
        {
            var dimensions = new Dimensions(2, 2);

            dimensions.Rank.Should().Be(2);
        }
        
        [Test]
        public void DegreesOfFreedom_ThreeDimensions_ShouldBeThree()
        {
            var dimensions = new Dimensions(2, 2, 2);

            dimensions.Rank.Should().Be(3);
        }

        [Test]
        public void Copy_WhenCalled_ShouldBeEqualButNotSame()
        {
            var dimension = new Dimensions(3);

            var copy = dimension.Copy();

            copy.Should().NotBeSameAs(dimension);
            copy.Should().BeEquivalentTo(dimension);
        }

        [Test]
        public void ImplicitOperator_UShort_ShouldBeExpected()
        {
            Dimensions dimensions = 100;

            dimensions.Length.Should().Be(100);
        }

        [Test]
        public void ImplicitOperator_IntFromDimensions_ShouldBeExpected()
        {
            Dimensions dimensions = 1000;

            int length = dimensions;

            length.Should().Be(1000);
        }

        [Test]
        public void Indices_EmptyDimensions_ShouldBeEmpty()
        {
            var dimension = Dimensions.Empty;

            var indices = dimension.Indices();

            indices.Should().BeEmpty();
        }
        
        [Test]
        public void Indices_OneDimensions_ShouldBeExpected()
        {
            var dimension = new Dimensions(5);

            var indices = dimension.Indices().ToList();

            indices.Should().HaveCount(5);
            indices.Should().Contain("[0]");
            indices.Should().Contain("[1]");
            indices.Should().Contain("[2]");
            indices.Should().Contain("[3]");
            indices.Should().Contain("[4]");
        }
        
        [Test]
        public void Indices_TwoDimensions_ShouldBeExpected()
        {
            var dimension = new Dimensions(2, 2);

            var indices = dimension.Indices().ToList();

            indices.Should().HaveCount(4);
            indices.Should().Contain("[0,0]");
            indices.Should().Contain("[0,1]");
            indices.Should().Contain("[1,0]");
            indices.Should().Contain("[1,1]");
        }
        
        [Test]
        public void Indices_ThreeDimensions_ShouldBeExpected()
        {
            var dimension = new Dimensions(1, 1, 2);

            var indices = dimension.Indices().ToList();

            indices.Should().HaveCount(2);
            indices.Should().Contain("[0,0,0]");
            indices.Should().Contain("[0,0,1]");
        }

        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Dimensions.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Parse_Empty_ShouldBeEmpty()
        {
            var dimension = Dimensions.Parse(string.Empty);

            dimension.Should().Be(Dimensions.Empty);
        }

        [Test]
        public void Parse_SingleSpace_ShouldBeEmpty()
        {
            FluentActions.Invoking(() => Dimensions.Parse(" ")).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void Parse_MoreThanThreeDimensions_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(() => Dimensions.Parse("1 4 3 8")).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Parse_OneDimensionZero_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("0");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(0);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
            dimensions.Length.Should().Be(0);
            dimensions.Should().BeEquivalentTo(Dimensions.Empty);
        }

        [Test]
        public void Parse_OneDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
            dimensions.Length.Should().Be(3);
        }

        [Test]
        public void Parse_TwoDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3 10");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(10);
            dimensions.Z.Should().Be(0);
            dimensions.Length.Should().Be(30);
        }

        [Test]
        public void Parse_ThreeDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3 10 6");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(10);
            dimensions.Z.Should().Be(6);
            dimensions.Length.Should().Be(180);
        }

        [Test]
        public void TryParse_Null_ShouldBeNullAndFalse()
        {
            var dimensions = Dimensions.TryParse(null!);
            
            dimensions.Should().BeNull();
        }

        [Test]
        public void TryParse_Empty_ShouldBeNullAndFalse()
        {
            var dimensions = Dimensions.TryParse(string.Empty);
            
            dimensions.Should().BeNull();
        }

        [Test]
        public void TryParse_MoreThanThreeDimensions_ShouldBeNullAndFalse()
        {
            var dimensions = Dimensions.TryParse("1 4 3 8");
            
            dimensions.Should().BeNull();
        }
        
        [Test]
        public void TryParse_ValidPattern_ShouldNotBeNullAndTrue()
        {
            var dimensions = Dimensions.TryParse("1");
            
            dimensions.Should().NotBeNull();
            dimensions?.X.Should().Be(1);
            dimensions?.Y.Should().Be(0);
            dimensions?.Z.Should().Be(0);
            dimensions?.Length.Should().Be(1);
        }
        
        [Test]
        public void TryParse_TwoDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.TryParse("3 10");
            
            dimensions.Should().NotBeNull();
            dimensions?.X.Should().Be(3);
            dimensions?.Y.Should().Be(10);
            dimensions?.Z.Should().Be(0);
            dimensions?.Length.Should().Be(30);
        }

        [Test]
        public void TryParse_ThreeDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.TryParse("3 10, 6");
            
            dimensions.Should().NotBeNull();
            dimensions?.X.Should().Be(3);
            dimensions?.Y.Should().Be(10);
            dimensions?.Z.Should().Be(6);
            dimensions?.Length.Should().Be(180);
        }
        
        [Test]
        public void ToIndex_EmptyDimension_ShouldBeExpected()
        {
            var dimension = Dimensions.Empty;

            dimension.ToIndex().Should().Be("[]");
        }

        [Test]
        public void ToIndex_OneDimension_ShouldBeExpected()
        {
            var dimension = new Dimensions(1);

            dimension.ToIndex().Should().Be("[1]");
        }
        
        [Test]
        public void ToIndex_TwoDimension_ShouldBeExpected()
        {
            var dimension = new Dimensions(1, 1);

            dimension.ToIndex().Should().Be("[1,1]");
        }
        
        [Test]
        public void ToIndex_ThreeDimension_ShouldBeExpected()
        {
            var dimension = new Dimensions(1, 1, 1);

            dimension.ToIndex().Should().Be("[1,1,1]");
        }

        [Test]
        public void Equals_TypeOverloadEquals_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);
            var d2 = new Dimensions(5);

            var result = d1.Equals(d2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadSame_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals(d1);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadEquals_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);
            var d2 = (object)new Dimensions(5);

            var result = d1.Equals(d2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadSame_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals((object)d1);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals((object)null!);

            result.Should().BeFalse();
        }

        [Test]
        public void OpEquals_AreEqual_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);
            var d2 = new Dimensions(5);

            var result = d1 == d2;

            result.Should().BeTrue();
        }

        [Test]
        public void OpNotEquals_AreEqual_ShouldBeFalse()
        {
            var d1 = new Dimensions(5);
            var d2 = new Dimensions(5);

            var result = d1 != d2;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var d1 = new Dimensions(5);

            var result = d1.GetHashCode();

            result.Should().NotBe(0);
        }
    }
}