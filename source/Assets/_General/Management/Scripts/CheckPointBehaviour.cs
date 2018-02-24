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
			GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("CheckPointActivated");
			GetComponentInChildren<ParticleSystem>().Play();
            Destroy(this);
        }
    }
}
