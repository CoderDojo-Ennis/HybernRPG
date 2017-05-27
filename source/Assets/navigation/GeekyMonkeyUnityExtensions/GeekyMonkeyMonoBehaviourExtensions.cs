using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeekyMonkeyMonoBehaviourExtensions
{
    public static GmDelayPromise Delay(this MonoBehaviour mb, float delaySeconds, Action callback = null)
    {
        var promise = new GmDelayPromise { monobehaviour = mb };
        promise.coroutine = mb.StartCoroutine(WaitThenCallback(delaySeconds, 1, callback, promise));
        return promise;
    }

    public static GmDelayPromise Repeat(this MonoBehaviour mb, float delaySeconds, int times, Action callback = null)
    {
        var promise = new GmDelayPromise { monobehaviour = mb };
        promise.coroutine = mb.StartCoroutine(WaitThenCallback(delaySeconds, times, callback, promise));
        return promise;
    }

    public static GmDelayPromise Forever(this MonoBehaviour mb, float delaySeconds, Action callback = null)
    {
        var promise = new GmDelayPromise { monobehaviour = mb };
        promise.coroutine = mb.StartCoroutine(WaitThenCallback(delaySeconds, int.MaxValue, callback, promise));
        return promise;
    }

    private static IEnumerator WaitThenCallback(float seconds, int times, Action callback, GmDelayPromise promise)
    {
        for (var i = 0; i < times || times == int.MaxValue; i++)
        {
            yield return new WaitForSeconds(seconds);
            if (callback != null)
            {
                try
                {
                    callback();
                } catch (Exception ex)
                {
                    Debug.Log("Callback Error: " + ex.Message);
                }
            }
        }
        promise.Done();
    }

    public static GameObject[] GetChildrenWithTag(this MonoBehaviour mb, string tag, bool includeInactive)
    {
        var childTransforms = mb.GetComponentsInChildren<Transform>(includeInactive);
        List<GameObject> taggedChildren = new List<GameObject>();
        for(var i = 0; i < childTransforms.Length; i++)
        {
            if (childTransforms[i].CompareTag(tag))
            {
                taggedChildren.Add(childTransforms[i].gameObject);
            }
        }

        return taggedChildren.ToArray();
    }

    static public GameObject FindChildByName(this MonoBehaviour mb, string childName)
    {
        Transform[] ts = mb.transform.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (Transform t in ts)
        {
            if (t.gameObject.name == childName)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}

public class GmDelayPromise
{
    internal Coroutine coroutine;
    internal MonoBehaviour monobehaviour;
    private Action then;

    public void Abort()
    {
        this.monobehaviour.StopCoroutine(this.coroutine);
    }

    public void Then(Action thenCallback)
    {
        this.then = thenCallback;
    }

    internal void Done()
    {
        if (this.then != null)
        {
            this.then();
        }
    }
}
