﻿using System;
using System.Globalization;

namespace L5Sharp.Core;

/// <summary>
/// A configurable property of a <see cref="Task"/> that specified how long a task can rung before triggering a major fault.
/// </summary>
/// <remarks>
/// <see cref="Watchdog"/> is a simple float value that must be between 0.1 and 2,000,000.0 ms.
/// Attempting to set the <see cref="Watchdog"/> to a value outside that range will result in an
/// <see cref="ArgumentOutOfRangeException"/>.
/// This parameter will control how long a task can rung before triggering a major fault.
/// </remarks>
public readonly struct Watchdog : IEquatable<Watchdog>, ILogixParsable<Watchdog>
{
    private readonly float _value;

    /// <summary>
    /// Creates a new instance of <c>Watchdog</c> with the specified value.
    /// </summary>
    /// <param name="value">The value of the watchdog in milliseconds.
    /// Must be a value between 0.1 and 2M.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throw when the provided value is not within the
    /// specified range.</exception>
    public Watchdog(float value)
    {
        if (value is < 0.1f or > 2000000.0f)
            throw new ArgumentOutOfRangeException(nameof(value),
                "Watchdog must be value between 0.1 and 2,000,000.0 ms");

        _value = value;
    }

    /// <summary>
    /// Creates a default 500ms <see cref="Watchdog"/> value.
    /// </summary>
    /// <returns>A new <see cref="Watchdog"/> value.</returns>
    public static Watchdog Default() => new(500);

    /// <summary>
    /// Converts a <see cref="Watchdog"/> to a <see cref="float"/>.
    /// </summary>
    /// <param name="watchdog">The value to convert.</param>
    /// <returns>A <see cref="float"/> value.</returns>
    public static implicit operator float(Watchdog watchdog) => watchdog._value;

    /// <summary>
    /// Converts a <see cref="float"/> to a <see cref="Watchdog"/>.
    /// </summary>
    /// <param name="watchdog">The value to convert.</param>
    /// <returns>A <see cref="Watchdog"/> value.</returns>
    public static implicit operator Watchdog(float watchdog) => new(watchdog);

    /// <summary>
    /// Parses a string into a <see cref="Watchdog"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The <see cref="Watchdog"/> representing the parsed value.</returns>
    public static Watchdog Parse(string value) => float.Parse(value);

    /// <summary>
    /// Tries to parse a string into a <see cref="Watchdog"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="Watchdog"/> representing the parsed value if successful; Otherwise; <c>default</c>.</returns>
    public static Watchdog TryParse(string? value) =>
        float.TryParse(value, out var result) ? new Watchdog(result) : default;

    /// <inheritdoc />
    public override string ToString() => _value.ToString(CultureInfo.CurrentCulture);

    /// <inheritdoc />
    public bool Equals(Watchdog other) => _value.Equals(other._value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Watchdog other => _value.Equals(other._value),
            ValueType other =>  _value.Equals(Convert.ChangeType(other, typeof(float))),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Watchdog left, Watchdog right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Watchdog left, Watchdog right) => !Equals(left, right);
}