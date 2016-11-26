using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {
    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

		if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("aiming")) {
            Debug.Log("player is aiming");
		}
	}
}
