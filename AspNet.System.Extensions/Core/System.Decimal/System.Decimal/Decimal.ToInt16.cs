// Copyright (c) 2015 ZZZ Projects. All rights reserved
// Licensed under MIT License (MIT) (https://github.com/zzzprojects/Z.ExtensionMethods)
// Website: http://www.zzzprojects.com/
// Feedback / Feature Requests / Issues : http://zzzprojects.uservoice.com/forums/283927
// All ZZZ Projects products: Entity Framework Extensions / Bulk Operations / Extension Methods /Icon Library

using System;

public static partial class Extensions
{
    /// <summary>
    ///     Converts the value of the specified  to the equivalent 16-bit signed integer.
    /// </summary>
    /// <param name="value">The decimal number to convert.</param>
    /// <returns>A 16-bit signed integer equivalent to .</returns>
    public static Int16 ToInt16(this Decimal value)
    {
        return Decimal.ToInt16(value);
    }
}