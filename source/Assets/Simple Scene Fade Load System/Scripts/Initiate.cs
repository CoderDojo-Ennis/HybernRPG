using UnityEngine;

public static class Initiate {
    //Create Fader object and assing the fade scripts and assign all the variables
    public static void Fade (string scene,Color col,float damp, float alpha=1, bool isFadeIn=true){
		GameObject init = new GameObject ();
		init.name = "Fader";
		init.AddComponent<Fader> ();
		Fader scr = init.GetComponent<Fader> ();
		scr.fadeDamp = damp;
		scr.fadeScene = scene;
		scr.fadeColor = col;
		scr.start = true;
		scr.isFadeIn = isFadeIn;
		scr.alpha = alpha;
	}
}
