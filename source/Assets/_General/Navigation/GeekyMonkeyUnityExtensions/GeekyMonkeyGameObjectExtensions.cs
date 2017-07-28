using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeekyMonkeyGameobjectExtensions
{
    public static void DeleteAllChildren(this GameObject go)
    {
        foreach (Transform t in go.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }

    public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag)
        where T : Component
    {
        var results = new List<T>();

        if (gameObject.CompareTag(tag))
        {
            results.Add(gameObject.GetComponent<T>());
        }

        foreach (Transform t in gameObject.transform)
        {
            results.AddRange(t.gameObject.GetComponentsInChildrenWithTag<T>(tag));
        }

        return results.ToArray();
    }

    public static T GetComponentInParents<T>(this GameObject gameObject)
        where T : Component
    {
        for (Transform t = gameObject.transform; t != null; t = t.parent)
        {
            T result = t.GetComponent<T>();
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public static T[] GetComponentsInParents<T>(this GameObject gameObject)
        where T : Component
    {
        var results = new List<T>();
        for (Transform t = gameObject.transform; t != null; t = t.parent)
        {
            T result = t.GetComponent<T>();
            if (result != null)
            {
                results.Add(result);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// the set of layers that GameObject can collide against.
    /// </summary>
    /// <param name="gameObject">The game object</param>
    /// <param name="layer">If omitted, it uses the layer of the calling GameObject, which is the most common/intuitive case. But you can specify a layer and it’ll hand you the collision mask for that layer instead.</param>
    /// <returns></returns>
    public static int GetCollisionMask(this GameObject gameObject, int layer = -1)
    {
        if (layer == -1)
        {
            layer = gameObject.layer;
        }

        int mask = 0;
        for (int i = 0; i < 32; i++)
        {
            mask |= (Physics.GetIgnoreLayerCollision(layer, i) ? 0 : 1) << i;
        }

        return mask;
    }
}
