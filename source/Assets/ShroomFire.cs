using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityStandardAssets._2D;

public class ShroomFire : MonoBehaviour {
    public Animator anim;
    public float visionRange;
    public CameraFollow cameraFollow;
    public WhiteFlash whiteFlash;

    void Start () {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        whiteFlash = GameObject.Find("WhiteExplosionEffect").GetComponent<WhiteFlash>();
    }

	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Good") {
            anim.SetBool("fire", true);
            this.Delay(1f, coll.gameObject.GetComponent<PlayerStats>().Die);
            this.Delay(1f, destroy);
            this.Delay(0.5f, whiteFlash.Explode);
            StartCoroutine(cameraFollow.MyRoutine(5f, 0.1f, 0.1f));
        }
    }

    public void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Good") {
            
            anim.SetBool("fire", false);
        }
    }
    private void destroy() {
        Destroy(gameObject);
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
