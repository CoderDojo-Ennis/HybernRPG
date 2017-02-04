using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
	//Manages Health
	public GameObject HealthOverview;
	GameObject Health1;
	GameObject Health2;
	GameObject Health3;
	
	public int MaxHealth;
	public int CurrentHealth;
	public int Damage;
	
	public bool IsDead;
	
	void Start () 
	{
		Health1 = HealthOverview.transform.GetChild(0).gameObject;
		Health2 = HealthOverview.transform.GetChild(1).gameObject;
		Health3 = HealthOverview.transform.GetChild(2).gameObject;
		MaxHealth = 3;
		CurrentHealth = MaxHealth;
		IsDead = false;
	}
	
	void Update () 
	{
		HealthShown();
		if (Damage != 0)
		{
			CurrentHealth = CurrentHealth - Damage;
			Damage = 0;
		}
		
		if (CurrentHealth <= 0)
		{
			IsDead = true;
			//I presume something might happen if you die?
			//Someone else can do that.
		}
	}
	//Woefully inefficient, but it does work.
	void HealthShown ()
	{
		if (CurrentHealth <=0)
		{
			Health1.SetActive(false);
			Health2.SetActive(false);
			Health3.SetActive(false);
		}
		
		if (CurrentHealth == 1)
		{
			Health1.SetActive(true);
			Health2.SetActive(false);
			Health3.SetActive(false);
		}
		
		if (CurrentHealth == 2)
		{
			Health1.SetActive(true);
			Health2.SetActive(true);
			Health3.SetActive(false);
		}
		
		if (CurrentHealth == 3)
		{
			Health1.SetActive(true);
			Health2.SetActive(true);
			Health3.SetActive(true);
		}
	}
}
