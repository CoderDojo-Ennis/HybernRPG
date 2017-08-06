using System;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour
{
    public int CPName;

    void Awake()
    {
        CPName = Int32.Parse(this.name);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player Physics Parent")
        {
            SaveLoad.Save(CPName);
            Destroy(this.gameObject);
        }
    }
}
