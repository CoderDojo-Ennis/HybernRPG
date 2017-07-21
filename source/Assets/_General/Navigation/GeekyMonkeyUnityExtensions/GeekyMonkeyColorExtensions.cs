using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeekyMonkeyColorExtensions
{
    /// <summary>
    /// Convert a color to another color with a different alpha
    /// </summary>
    /// <param name="color">The original color</param>
    /// <param name="alpha">New alpha value</param>
    /// <returns></returns>
    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
