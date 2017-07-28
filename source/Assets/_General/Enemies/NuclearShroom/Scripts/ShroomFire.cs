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
    public DarkHaze darkHaze;
    public SpriteRenderer sr;
    private float lerp = 0;
    public float explodeScale;
    void Start () {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        whiteFlash = GameObject.Find("WhiteExplosionEffect").GetComponent<WhiteFlash>();
        darkHaze = GameObject.Find("AfterExplosionEffect").GetComponent<DarkHaze>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

	void Update () {
		if(anim.GetBool("fire") == true) {
            anim.Play("fire");
            lerp += Time.deltaTime;
            sr.color = Color.Lerp(Color.white, Color.red, lerp);
            transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(explodeScale, 1f, explodeScale), lerp);
        }
        if (lerp >= 1f) {
            whiteFlash.Explode();
            StartCoroutine(cameraFollow.MyRoutine(2f, 0.1f, 0.1f));
            this.Delay(1f, destroy);
            darkHaze.Explode();
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Good") {
            anim.SetBool("fire", true);
//<<<<<<< Updated upstream
            //GetComponentInChildren<SpriteRenderer>().color = Color.white;

            this.Delay(0.25f, () => {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Nuclear Explosion");
            });
            this.Delay(1f, coll.gameObject.GetComponent<PlayerStats>().Die);/*
            this.Delay(2f, () => {
                burn(coll.gameObject);
            });
=======
            this.Delay(1f, coll.gameObject.GetComponent<PlayerStats>().Die);
            this.Delay(2f, burn(coll.gameObject));
>>>>>>> Stashed changes
            this.Delay(1f, destroy);
            this.Delay(0.75f, whiteFlash.Explode);
            StartCoroutine(cameraFollow.MyRoutine(5f, 0.1f, 0.1f));
            */
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
    //<<<<<<< Updated upstream
    public void burn(GameObject go) {
        SpriteRenderer[] spriteR;
        spriteR = go.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in spriteR) {
            sr.color = Color.black;
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
