using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShark : MonoBehaviour {
    private float xScale = 1;
    public float swimSpeed = 1;
    public Collider2D collider;
    private Rigidbody2D rb;
    public SharkActions sharkAction;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sharkAction = SharkActions.underLava;
        collider = GetComponent<Collider2D>();
    }
    public enum SharkActions {
        underLava,
        swimmingLeft,
        swimmingRight,
        laserLeft,
        laserRight,
        laserCenter,
        jumpLeft,
        jumpRight,
        jumpCenter

    }
    public enum LaserActions {

    }
    // Update is called once per frame
    void FixedUpdate () {
        switch (sharkAction) {
            case SharkActions.underLava:
                collider.density = 5f;
                break;

            case SharkActions.swimmingLeft:
                collider.density = 4f;
                xScale = 1;
                rb.AddForce(Vector2.right * ((-swimSpeed - rb.velocity.x)), ForceMode2D.Impulse);
                break;
            case SharkActions.swimmingRight:
                collider.density = 4f;
                xScale = -1;
                rb.AddForce(Vector2.right * ((swimSpeed - rb.velocity.x)), ForceMode2D.Impulse);
                break;
            case SharkActions.jumpCenter:
                break;
        }
        transform.localScale = new Vector3(xScale, 1, 1);
	}
}
