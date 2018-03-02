using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShark : MonoBehaviour {
    private float xScale = 1;
    public float swimSpeed = 1;
    public Collider2D col;
    private Rigidbody2D rb;
    public SharkActions sharkAction;
    private Vector2 upRight = (Vector3.up + Vector3.right).normalized;
    private Vector2 upLeft = (Vector3.up + Vector3.left).normalized;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sharkAction = SharkActions.underLava;
        col = GetComponent<Collider2D>();
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
                col.density = 4.9f;
                break;

            case SharkActions.swimmingLeft:
                col.density = 4f;
                xScale = 1;
                rb.AddForce(Vector2.right * ((-swimSpeed - rb.velocity.x)), ForceMode2D.Impulse);
                break;
            case SharkActions.swimmingRight:
                col.density = 4f;
                xScale = -1;
                rb.AddForce(Vector2.right * ((swimSpeed - rb.velocity.x)), ForceMode2D.Impulse);
                break;
            case SharkActions.jumpCenter:
                rb.AddForce(Vector2.up * 500f, ForceMode2D.Impulse);
                break;
            case SharkActions.jumpLeft:
                rb.AddForce(upLeft * 1000f, ForceMode2D.Impulse);
                break;
            case SharkActions.jumpRight:
                rb.AddForce(upRight * 1000f, ForceMode2D.Impulse);
                //rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                break;
        }
        transform.localScale = new Vector3(xScale, 1, 1);
        if(sharkAction == SharkActions.jumpCenter) {
            sharkAction = SharkActions.underLava;
        }
        if(sharkAction == SharkActions.jumpLeft) {
            sharkAction = SharkActions.swimmingLeft;
        }
        if(sharkAction == SharkActions.jumpRight) {
            sharkAction = SharkActions.swimmingRight;
        }
	}
}
