using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    float y;
    public float bobbingDistance;

    [Range(0.0f, 1.0f)]
    public float maxGlow;

    public void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.name == "Player Physics Parent") {
            coll.gameObject.GetComponent<PlayerStats>().TakeDamage(-1);
            Destroy(gameObject);
        }
    }

    void Start () {
        y = transform.position.y;
    }
	
	void Update () {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Material mat = renderer.material;
        float emission = Mathf.PingPong(Time.time, maxGlow);
        Color baseColor = Color.green;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        mat.SetColor ("_EmissionColor", finalColor);

        transform.position = new Vector3(transform.position.x, y + Mathf.PingPong(Time.time / 8, bobbingDistance), transform.position.z);
        //Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(explodeScale, 1f, explodeScale), lerp);
    }
}
