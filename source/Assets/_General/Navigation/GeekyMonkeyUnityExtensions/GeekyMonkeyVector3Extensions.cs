using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeekyMonkeyVector3Extensions
{
    public static void SetX(this Vector3 v, float x)
    {
        v.Set(x, v.y, v.z);
    }

    public static void SetY(this Vector3 v, float y)
    {
        v.Set(v.x, y, v.z);
    }

    public static void SetZ(this Vector3 v, float z)
    {
        v.Set(v.x, v.y, z);
    }

    public static Vector3 GetMeanVector(this IList<Vector3> positions)
    {
        int vectorCount = positions.Count;
        if (vectorCount == 0)
        {
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;
        for (int i = 0; i < vectorCount; i++)
        {
            sum += positions[i]; 
        }
        return sum / vectorCount;
    }

    /// <summary>
    /// Is this vector nearly equal to another vector within a cube of variance
    /// </summary>
    /// <param name="a">This vectorr</param>
    /// <param name="b">Vector to compare to</param>
    /// <param name="maxDifference">Allowed difference</param>
    /// <returns>True if similar</returns>
    public static Boolean Approximately(this Vector3 a, Vector3 b, float maxDifference)
    {
        if (!a.x.Approximately(b.x, maxDifference))
        {
            return false;
        }
        if (!a.y.Approximately(b.y, maxDifference))
        {
            return false;
        }
        return (a.z.Approximately(b.z, maxDifference));
    }

    /// <summary>
    /// Generate an integer hash for a vector 3 rounded to the nearest 0.1 unit. Will only work for values up to 1000
    /// </summary>
    /// <param name="v">The vector 3</param>
    /// <returns></returns>
    public static ulong Hash(this Vector3 v, float accuracy = 0.5f)
    {
        ulong hash;
        unchecked       
        {
            int x2 = Mathf.RoundToInt(v.x / accuracy);
            int y2 = Mathf.RoundToInt(v.y / accuracy);
            int z2 = Mathf.RoundToInt(v.z / accuracy);
            hash = (((ulong)Math.Abs(x2)) << 43);
            hash |= (((ulong)Math.Abs(y2)) << 23);
            hash |= (((ulong)Math.Abs(z2)) << 3);

            hash |= ((x2 < 0) ? 0u : 1u);
            hash |= ((y2 < 0) ? 0u : 2u);
            hash |= ((z2 < 0) ? 0u : 4u);
        }
        return hash;
    }

    /// <summary>
    /// Rotate this point around a pivot point
    /// </summary>
    /// <param name="point">This point</param>
    /// <param name="pivot">Pivot point</param>
    /// <param name="angles">Angled</param>
    /// <returns>Transformed point</returns>
    public static Vector3 RotatePointAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
    /// <summary>
    /// Rotate this point around a pivot point
    /// </summary>
    /// <param name="point">This point</param>
    /// <param name="pivot">Pivot point</param>
    /// <param name="angles">Angled</param>
    /// <returns>Transformed point</returns>
    public static Vector3 RotatePointAroundPivot(this Vector3 point, Vector3 pivot, Quaternion angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = angles * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}
