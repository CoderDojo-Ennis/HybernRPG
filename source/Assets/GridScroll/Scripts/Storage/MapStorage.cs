using UnityEngine;
using System.Collections;

public class MapStorage : MonoBehaviour
{
	//Contained within here is the array storing the map data.
	//Each value, for example, 1, 0 or 5, corresponds to abstract
	//particular tile texture which is displayed by the game.
	public int[] map;
	void Start ()
	{
		map = new int[30];
		map[0] =0;
		map[1] =1;
		map[2] =2;
		map[3] =3;
		map[4] =4;
		map[5] =5;
		map[6] =6;
		map[7] =7;
		map[8] =8;
		map[9] =9;
		map[10] =0;
		map[11] =0;
		map[12] =1;
		map[13] =2;
		map[14] =8;
		map[15] =8;
		map[16] =2;
		map[17] =1;
		map[18] =0;
		map[19] =0;
		map[20] =9;
		map[21] =8;
		map[22] =7;
		map[23] =6;
		map[24] =5;
		map[25] =4;
		map[26] =3;
		map[27] =2;
		map[28] =1;
		map[29] =0;
	}
}
