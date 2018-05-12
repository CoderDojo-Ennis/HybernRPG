﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public GameObject orbPrefab;
	private Vector2 StartOffset = new Vector2(20, 20);
	public Sprite bigOrb;
	public Sprite smallOrb;
	private List<GameObject> orbs;
	

	void Awake()
	{
		orbs = new List<GameObject>();
	}

	public void DisplayHealth(int health)
	{
		//1 orb has 2 states, so a given value of health can
		//be displayed with half as many orbs
		if(health <= 0)
		{
			//Clear list
			for(int counter = 0; counter < orbs.Count; counter++)
			{
				GameObject.Destroy(orbs[counter]);
			}
			orbs.Clear();
			return;
		}
		
		//How many orbs do we need
		int desiredOrbsAmount = (int) Mathf.Ceil((float)health/2f);
		
		//If we already have some of the orbs we need 
		if(orbs.Count < desiredOrbsAmount)
		{
			//Set lastest created orb to bigOrb
			if(orbs.Count- 1 >= 0)
			{
				orbs[orbs.Count- 1].GetComponent<Image>().sprite = bigOrb;
			}
			//Make the remaining ones
			Vector2 offset = new Vector2(33, 0);
			for(int counter = orbs.Count; counter < desiredOrbsAmount; counter++)
			{
				//Vector2 StartingPos = new Vector2(-(Screen.width / 2) + StartOffset.x, -(Screen.height / 2) + StartOffset.y);
				GameObject orb = GameObject.Instantiate(orbPrefab, StartOffset + offset * counter, Quaternion.identity, transform);
				orb.GetComponent<RectTransform>().position = StartOffset + (offset * counter);
				
				orbs.Add(orb);
			}
			//Decide if last orb should be a big orb or a small orb
			if(health%2 == 0)
			{
				//Even, big orb
				orbs[orbs.Count -1].GetComponent<Image>().sprite = bigOrb;
			}
			else
			{
				//Odd, small orb
				orbs[orbs.Count -1].GetComponent<Image>().sprite = smallOrb;
			}
		}
		//If we have too many orbs
		if(orbs.Count > desiredOrbsAmount)
		{
			for(int counter = orbs.Count; counter > desiredOrbsAmount; counter--)
			{
				GameObject.Destroy(orbs[orbs.Count -1]);
				orbs.RemoveAt(orbs.Count-1);
			}
			//Decide if last orb should be a big orb or a small orb
			if(orbs.Count- 1 >= 0)
			{
				if(health%2 == 0)
				{
					//Even, big orb
					orbs[orbs.Count -1].GetComponent<Image>().sprite = bigOrb;
				}
				else
				{
					//Odd, small orb
					orbs[orbs.Count-1].GetComponent<Image>().sprite = smallOrb;
				}
			}
		}
		//If we have just enough orbs
		if(orbs.Count == desiredOrbsAmount)
		{
			//Decide if last orb should be a big orb or a small orb
			if(orbs.Count- 1 >= 0)
			{
				if(health%2 == 0)
				{
					//Even, big orb
					orbs[orbs.Count -1].GetComponent<Image>().sprite = bigOrb;
				}
				else
				{
					//Odd, small orb
					orbs[orbs.Count-1].GetComponent<Image>().sprite = smallOrb;
				}
			}
		}
	}
}
