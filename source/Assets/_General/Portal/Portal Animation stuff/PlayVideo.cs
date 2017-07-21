using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayVideo : MonoBehaviour {

    public MovieTexture movie;
	
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        movie.Play();
	}
	
	
	void Update () {
		
	}
}
