﻿using System.Diagnostics;
using System.Globalization;
using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data.Atomics;

[TestFixture]
public class RealTests
{
    private float _random;

    [SetUp]
    public void Setup()
    {
        var fixture = new Fixture();
        _random = fixture.Create<float>();
    }
        
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var type = new REAL();

        type.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var type = new REAL();

        type.Should().NotBeNull();
        type.Should().Be(0);
        type.Name.Should().Be(nameof(REAL).ToUpper());
        type.Members.Should().HaveCount(0);
        type.Radix.Should().Be(Radix.Float);
    }

    [Test]
    public void New_Value_ShouldHaveExpectedValues()
    {
        var type = new REAL(_random);

        type.Should().Be(_random);
    }

    [Test]
    public void New_ValidRadix_ShouldHaveExpectedValues()
    {
        var type = new REAL(Radix.Exponential);

        type.Radix.Should().Be(Radix.Exponential);
        type.ToString().Should().Be("0.00000000e+000");
    }

    [Test]
    public void Test_ValidRadix_ShouldHaveExpectedValues()
    {
        var type = new REAL(1.23f, Radix.Exponential);

        type.Radix.Should().Be(Radix.Exponential);
        type.ToString().Should().Be("1.23000002e+000");
    }

    [Test]
    public void New_NullRadix_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new REAL(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_InvalidRadix_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new REAL(Radix.Binary)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ValueAndRadix_ShouldHaveExpectedValues()
    {
        var type = new REAL(1.23f, Radix.Float);

        type.Should().Be(1.23f);
        type.Radix.Should().Be(Radix.Float);
    }

    [Test]
    public void New_ValueAndRadixNullRadix_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new REAL(1.23f, null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_ValueAndRadixInvalidRadix_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new REAL(1.23f, Radix.Binary)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void As_AtomicType_ShouldNotBeNull()
    {
        var type = new REAL();

        var atomic = type.As<AtomicData>();

        atomic.Should().NotBeNull();
    }

    [Test]
    public void As_InvalidType_ShouldThrowInvalidCastException()
    {
        var type = new REAL();

        FluentActions.Invoking(() => type.As<StructureData>()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Clone_WhenCalled_ReturnsDifferentInstance()
    {
        var type = new REAL();

        var clone = type.Clone();

        clone.Should().NotBeSameAs(type);
    }

    [Test]
    public void Clone_WhenCalled_ShouldHaveSameValue()
    {
        var type = new REAL(1.23f);

        var clone = type.Clone();

        clone.Should().Be(1.23f);
    }

    [Test]
    public Task Serialize_Default_ShouldBeValid()
    {
        var type = new REAL();

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_Value_ShouldBeValid()
    {
        var type = new REAL(1.23f);

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_ValueAndRadix_ShouldBeValid()
    {
        var type = new REAL(1.23f, Radix.Exponential);

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void ToBoolean_WhenCalled_ShouldBeExpectedValue()
    {
        var type = new REAL() as IConvertible;

        var result = type.ToBoolean(CultureInfo.InvariantCulture);

        result.Should().BeFalse();
    }

    [Test]
    public void ToByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<byte>();
        var expected = (byte)Convert.ChangeType(value, typeof(byte));
        var type = new REAL(value) as IConvertible;

        var result = type.ToByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToChar_WhenCalled_ShouldBeExpectedValue()
    {
        var type = new REAL(_random) as IConvertible;

        FluentActions.Invoking(() => type.ToChar(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDateTime_WhenCalled_ShouldThrowInvalidCastException()
    {
        var type = new REAL(_random) as IConvertible;

        FluentActions.Invoking(() => type.ToDateTime(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDecimal_WhenCalled_ShouldThrowInvalidCastException()
    {
        var type = new REAL(_random) as IConvertible;

        FluentActions.Invoking(() => type.ToDecimal(CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToDouble_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (double)Convert.ChangeType(_random, typeof(double));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToDouble(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (short)Convert.ChangeType(_random, typeof(short));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (int)Convert.ChangeType(_random, typeof(int));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (long)Convert.ChangeType(_random, typeof(long));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSByte_WhenCalled_ShouldBeExpectedValue()
    {
        var fixture = new Fixture();
        var value = fixture.Create<sbyte>();
        var expected = (sbyte)Convert.ChangeType(value, typeof(sbyte));
        var type = new REAL(value) as IConvertible;

        var result = type.ToSByte(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToSingle_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (float)Convert.ChangeType(_random, typeof(float));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToSingle(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }
        
    [Test]
    public void ToString_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = _random.ToString("F1");
        var type = new REAL(_random) as IConvertible;

        var result = type.ToString(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt16_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ushort)Convert.ChangeType(_random, typeof(ushort));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToUInt16(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt32_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (uint)Convert.ChangeType(_random, typeof(uint));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToUInt32(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToUInt64_WhenCalled_ShouldBeExpectedValue()
    {
        var expected = (ulong)Convert.ChangeType(_random, typeof(ulong));
        var type = new REAL(_random) as IConvertible;

        var result = type.ToUInt64(CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Boolean_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;

        var result = (bool)type.ToType(typeof(bool), CultureInfo.InvariantCulture);

        result.Should().BeTrue();
    }

    [Test]
    public void ToType_Byte_ShouldBeExpectedValue()
    {
        const byte expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (byte)type.ToType(typeof(byte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Char_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;
            
        FluentActions.Invoking(() => type.ToType(typeof(char), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_DateTime_ShouldThrowInvalidCastException()
    {
        var type = new REAL(1) as IConvertible;

        FluentActions.Invoking(() => type.ToType(typeof(DateTime), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Decimal_ShouldThrowInvalidCastException()
    {
        var type = new REAL(1) as IConvertible;

        FluentActions.Invoking(() => type.ToType(typeof(decimal), CultureInfo.InvariantCulture))
            .Should().Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Double_ShouldBeExpectedValue()
    {
        const double expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (double)type.ToType(typeof(double), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int16_ShouldBeExpectedValue()
    {
        const short expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (short)type.ToType(typeof(short), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int32_ShouldBeExpectedValue()
    {
        const int expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (int)type.ToType(typeof(int), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Int64_ShouldBeExpectedValue()
    {
        const long expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (long)type.ToType(typeof(long), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Sbyte_ShouldBeExpectedValue()
    {
        const sbyte expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (sbyte)type.ToType(typeof(sbyte), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_Float_ShouldBeExpectedValue()
    {
        const float expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (float)type.ToType(typeof(float), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_String_ShouldBeExpectedValue()
    {
        const string expected = "1.23";
        var type = new REAL(1.23f) as IConvertible;

        var result = (string)type.ToType(typeof(string), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt16_ShouldBeExpectedValue()
    {
        const ushort expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (ushort)type.ToType(typeof(ushort), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UInt32_ShouldBeExpectedValue()
    {
        const uint expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (uint)type.ToType(typeof(uint), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DbNull_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;

        FluentActions.Invoking(() => type.ToType(typeof(DBNull), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_Null_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;

        FluentActions.Invoking(() => type.ToType(null!, CultureInfo.InvariantCulture)).Should()
            .Throw<ArgumentNullException>();
    }

    [Test]
    public void ToType_Invalid_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;

        FluentActions.Invoking(() => type.ToType(typeof(StructureData), CultureInfo.InvariantCulture)).Should()
            .Throw<InvalidCastException>();
    }

    [Test]
    public void ToType_UInt64_ShouldBeExpectedValue()
    {
        const ulong expected = 1;
        var type = new REAL(1) as IConvertible;

        var result = (ulong)type.ToType(typeof(ulong), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_BOOL_ShouldBeExpectedValue()
    {
        var type = new REAL(1) as IConvertible;

        var result = (BOOL)type.ToType(typeof(BOOL), CultureInfo.InvariantCulture);

        result.Should().Be(true);
    }

    [Test]
    public void ToType_SINT_ShouldBeExpectedValue()
    {
        var expected = new SINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (SINT)type.ToType(typeof(SINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_INT_ShouldBeExpectedValue()
    {
        var expected = new REAL(1);
        var type = new REAL(1) as IConvertible;

        var result = (INT)type.ToType(typeof(INT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_DINT_ShouldBeExpectedValue()
    {
        var expected = new DINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (DINT)type.ToType(typeof(DINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LINT_ShouldBeExpectedValue()
    {
        var expected = new LINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (LINT)type.ToType(typeof(LINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_REAL_ShouldBeExpectedValue()
    {
        var expected = new REAL(1);
        var type = new REAL(1) as IConvertible;

        var result = (REAL)type.ToType(typeof(REAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_USINT_ShouldBeExpectedValue()
    {
        var expected = new USINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (USINT)type.ToType(typeof(USINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UINT_ShouldBeExpectedValue()
    {
        var expected = new UINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (UINT)type.ToType(typeof(UINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_UDINT_ShouldBeExpectedValue()
    {
        var expected = new UDINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (UDINT)type.ToType(typeof(UDINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_ULINT_ShouldBeExpectedValue()
    {
        var expected = new ULINT(1);
        var type = new REAL(1) as IConvertible;

        var result = (ULINT)type.ToType(typeof(ULINT), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void ToType_LREAL_ShouldBeExpectedValue()
    {
        var expected = new LREAL(1);
        var type = new REAL(1) as IConvertible;

        var result = (LREAL)type.ToType(typeof(LREAL), CultureInfo.InvariantCulture);

        result.Should().Be(expected);
    }

    [Test]
    public void Conversion_FromFloat_ShouldBeExpectedValue()
    {
        REAL type = _random;

        type.Should().Be(_random);
    }

    [Test]
    public void Conversion_ToFloat_ShouldBeExpectedValue()
    {
        var type = new REAL();

        float value = type;

        value.Should().Be(0);
    }

    [Test]
    public void Conversion_FromString_ShouldBeExpectedValue()
    {
        REAL value = "1.23";

        value.Should().Be(1.23f);
    }

    [Test]
    public void Conversion_ToString_ShouldBeExpectedValue()
    {
        var type = new REAL(1.23f);

        string value = type;

        value.Should().Be("1.23");
    }

    [Test]
    public void ToString_DefaultRadix_ShouldBeExpected()
    {
        var type = new REAL();

        var format = type.ToString();

        format.Should().Be("0.0");
    }

    [Test]
    public void ToString_OverloadedRadix_ShouldBeExpected()
    {
        var type = new REAL();

        var format = type.ToString(Radix.Exponential);

        format.Should().Be("0.00000000e+000");
    }

    [Test]
    [TestCase("1.23")]
    [TestCase("1.23e0")]
    public void Parse_ValidFormat_ShouldBeExpectedValue(string value)
    {
        var type = REAL.Parse(value);

        type.Should().Be(1.23f);
    }

    [Test]
    public void Equals_AreEqual_ShouldBeTrue()
    {
        var first = new REAL();
        var second = new REAL();

        var result = first.Equals(second);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_AreNotEqual_ShouldBeFalse()
    {
        var first = new REAL(1);
        var second = new REAL(2);

        var result = first.Equals(second);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_AreSame_ShouldBeTrue()
    {
        var first = new REAL();

        // ReSharper disable once EqualExpressionComparison this is the test.
        var result = first.Equals(first);

        result.Should().BeTrue();
    }


    [Test]
    public void Equals_Null_ShouldBeFalse()
    {
        var first = new REAL();

        var result = first.Equals(null);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_DifferentAtomicEqualValue_ShouldBeTrue()
    {
        var first = new REAL(1);
        var second = new INT(1);

        // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
        var result = first.Equals(second);

        result.Should().BeTrue();
    }

    [Test]
    public void Equals_DifferentAtomicNotEqualValue_ShouldBeFalse()
    {
        var first = new REAL(1);
        var second = new INT(2);

        // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
        var result = first.Equals(second);

        result.Should().BeFalse();
    }

    [Test]
    public void Equals_InvalidType_ShouldBeFalse()
    {
        var first = new REAL(0);

        // ReSharper disable once SuspiciousTypeConversion.Global this method supports different atomic types.
        var result = first.Equals(new TIMER());

        result.Should().BeFalse();
    }

    [Test]
    [TestCase(1000)]
    [TestCase(10000)]
    [TestCase(100000)]
    [TestCase(1000000)]
    public void Equals_OverLargeCollection_ShouldWorkFairlyFast(int capacity)
    {
        var stopwatch = new Stopwatch();

        var range = Enumerable.Range(0, capacity).Select(_ => new REAL(1.23f)).ToList();

        stopwatch.Start();
        var result = range.Where(v => v == new REAL(1.23f)).ToList();
        stopwatch.Stop();

        result.Count.Should().Be(capacity);
    }
        
    [Test]
    public void EquivalentTo_AreEqual_ShouldBeTrue()
    {
        var first = new REAL(1.23f);
        var second = new REAL(1.23f);

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }
        
    [Test]
    public void EquivalentTo_AreNotEqual_ShouldBeFalse()
    {
        var first = new REAL(1.23f);
        var second = new REAL(1.32f);

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void GetHashCode_RandomValue_ShouldBeHashOfValue()
    {
        var type = new REAL(_random);

        var hash = type.GetHashCode();

        hash.Should().Be(_random.GetHashCode());
    }

    [Test]
    public void GetTypeCode_WhenCalled_ShouldBeObjectType()
    {
        var type = new REAL() as IConvertible;

        var code = type.GetTypeCode();

        code.Should().Be(TypeCode.Object);
    }

    [Test]
    public void CompareTo_Equal_ShouldBeZero()
    {
        var first = new REAL();
        var second = new REAL();

        var compare = first.CompareTo(second);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_Same_ShouldBeZero()
    {
        var type = new REAL();

        var compare = type.CompareTo(type);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_ValueTypeEqual_ShouldBeZero()
    {
        var type = new REAL();

        var compare = type.CompareTo(0);

        compare.Should().Be(0);
    }

    [Test]
    public void CompareTo_Null_ShouldBeOne()
    {
        var type = new REAL();

        var compare = type.CompareTo(null);

        compare.Should().Be(1);
    }

    [Test]
    public void CompareTo_AGreater_ShouldBeOne()
    {
        var a = new REAL(2);
        var b = new REAL(1);

        var result = a.CompareTo(b);

        result.Should().Be(1);
    }

    [Test]
    public void CompareTo_BGreater_ShouldBeNegativeOne()
    {
        var a = new REAL(1);
        var b = new REAL(2);

        var result = a.CompareTo(b);

        result.Should().Be(-1);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithEqualValue_ShouldBeZero()
    {
        var a = new REAL(1);
        var b = new INT(1);

        var result = a.CompareTo(b);

        result.Should().Be(0);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithLesserValue_ShouldBeOne()
    {
        var a = new REAL(1);
        var b = new INT(0);

        var result = a.CompareTo(b);

        result.Should().Be(1);
    }

    [Test]
    public void CompareTo_DifferentAtomicWithGreaterValue_ShouldBeNegativeOne()
    {
        var a = new REAL(1);
        var b = new INT(2);

        var result = a.CompareTo(b);

        result.Should().Be(-1);
    }

    [Test]
    public void CompareTo_InvalidType_ShouldThrowArgumentException()
    {
        var a = new REAL();
        var b = new TIMER();

        FluentActions.Invoking(() => a.CompareTo(b)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void OperatorEquals_AreEqual_ShouldBeTrue()
    {
        var first = new REAL();
        var second = new REAL();

        var result = first == second;

        result.Should().BeTrue();
    }

    [Test]
    public void OperatorEquals_AreNotEqual_ShouldBeFalse()
    {
        var first = new REAL(1);
        var second = new REAL(2);

        var result = first == second;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorNotEquals_AreEqual_ShouldBeFalse()
    {
        var first = new REAL();
        var second = new REAL();

        var result = first != second;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorNotEquals_AreNotEqual_ShouldBeTrue()
    {
        var first = new REAL(1);
        var second = new REAL(2);

        var result = first != second;

        result.Should().BeTrue();
    }

    [Test]
    public void OperatorGreaterThan_AGreaterThanB_ShouldBeTrue()
    {
        var a = new REAL(2);
        var b = new REAL(1);

        var result = a > b;

        result.Should().BeTrue();
    }

    [Test]
    public void OperatorGreaterThan_ALessThanB_ShouldBeFalse()
    {
        var a = new REAL(1);
        var b = new REAL(2);

        var result = a > b;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorLessThan_AGreaterThanB_ShouldBeFalse()
    {
        var a = new REAL(2);
        var b = new REAL(1);

        var result = a < b;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorLessThan_ALessThanB_ShouldBeTrue()
    {
        var a = new REAL(1);
        var b = new REAL(2);

        var result = a < b;

        result.Should().BeTrue();
    }

    [Test]
    public void OperatorGreaterThanOrEqualTo_AGreaterThanB_ShouldBeTrue()
    {
        var a = new REAL(2);
        var b = new REAL(1);

        var result = a >= b;

        result.Should().BeTrue();
    }

    [Test]
    public void OperatorGreaterThanOrEqualTo_ALessThanB_ShouldBeFalse()
    {
        var a = new REAL(1);
        var b = new REAL(2);

        var result = a >= b;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorLessThanOrEqualTo_AGreaterThanB_ShouldBeFalse()
    {
        var a = new REAL(2);
        var b = new REAL(1);

        var result = a <= b;

        result.Should().BeFalse();
    }

    [Test]
    public void OperatorLessThanOrEqualTo_ALessThanB_ShouldBeTrue()
    {
        var a = new REAL(1);
        var b = new REAL(2);

        var result = a <= b;

        result.Should().BeTrue();
    }
}