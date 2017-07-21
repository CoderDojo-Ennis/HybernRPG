using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class WhiteFlash : MonoBehaviour {

    public CanvasGroup myCG;
    private bool flash = false;

    void Update() {
        if (flash) {
            myCG.alpha = myCG.alpha - Time.deltaTime/10;
            if (myCG.alpha <= 0) {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    public void Explode() {
        flash = true;
        myCG.alpha = 1;
    }
}