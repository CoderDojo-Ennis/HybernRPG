using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHaze: MonoBehaviour {

    public CanvasGroup myCG;
    //private bool explosion = false;

    public void Explode() {
        //explosion = true;
        myCG.alpha = 1;
    }
}

