using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeekyMonkeyFloatExtensions
{
    /// <summary>
    /// Is this number nearly equal to another number
    /// </summary>
    /// <param name="a">This number</param>
    /// <param name="b">Number to compare to</param>
    /// <param name="maxDifference">Allowed difference</param>
    /// <returns>True if similar</returns>
    public static Boolean Approximately(this float a, float b, float maxDifference)
    {
        return ((a < b) ? (b - a) : (a - b)) <= maxDifference;
    }
}
