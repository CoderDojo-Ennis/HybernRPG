using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreParentXScale : MonoBehaviour {
	void LateUpdate()
	{
		//Hex value for sky colour E10FD300
			this.transform.localScale = this.transform.parent.localScale;
	}
}
