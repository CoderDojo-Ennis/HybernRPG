using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomFire : MonoBehaviour {
    public Animator anim;
    public float visionRange;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Good") {
            Debug.Log("gotcha");
            anim.SetBool("fire", true);
        }
    }
    public void OnTriggerExit2D(Collider2D coll) {
        //Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "Good") {
            Debug.Log("nooo");
            anim.SetBool("fire", false);
        }
    }
    void FixedUpdate() {
        anim = GetComponentInChildren<Animator>();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Good");
        float closestDist = visionRange * visionRange;
        GameObject bestMatch = null;
        for (int i = 0; i < targets.Length; i++) {
            float dist = (transform.position - targets[i].transform.position).sqrMagnitude;
            //which target is closest
            if (dist < closestDist) {
                closestDist = dist;
                bestMatch = targets[i]; 
                //is target on my right or left?
                if(targets[i].transform.position.x - transform.position.x > 0) {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                } else {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
        }
        //if there is a target that can be seen, start charging
        if (bestMatch == null) {
            anim.SetBool("charge", false);
            anim.SetBool("fire", false);
        } else {
            anim.SetBool("charge", true);
        }
    }
}
