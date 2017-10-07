using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour
{
    public Sprite sprite;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player Physics Parent")
        {
            GetComponent<SpriteRenderer>().sprite = sprite;

        }
    }

}