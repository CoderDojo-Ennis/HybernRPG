using UnityEngine;
using System.Collections;

public class aiming : MonoBehaviour {
    public Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponentInParent<Animator>();

    }
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 mousePos = Input.mousePosition;
		Vector3 dir = Camera.main.ScreenToWorldPoint (mousePos);
		dir = dir - transform.position;
		float a = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Debug.Log (a);
		if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("aiming")) {
			transform.rotation = Quaternion.AngleAxis (a+90, Vector3.forward);
			if (a > -90 && a < 90) {
				transform.root.localScale = new Vector3 (-1f, 1f, 1f);
			} else {
				transform.root.localScale = new Vector3 (1f, 1f, 1f);
			}
			//Debug.Log (a);

		}
	}
}
