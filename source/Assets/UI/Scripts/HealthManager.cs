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
    GameObject Health4;
    GameObject Health5;
    GameObject Health6;
    GameObject Health7;
    GameObject Health8;
	
	public int MaxHealth;
	public int CurrentHealth;
	public int Damage;
	
	public bool IsDead;
	
	void Start () 
	{
		Health1 = HealthOverview.transform.GetChild(0).gameObject;
		Health2 = HealthOverview.transform.GetChild(1).gameObject;
		Health3 = HealthOverview.transform.GetChild(2).gameObject;
		MaxHealth = 8;
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
            Health1.SetActive(false);
			IsDead = true;
            Application.Quit();
			//I presume something might happen if you die?
			//Someone else can do that.
		}
	}
    //Redone health system for 8 HP (not 3)
    //Not as inefficient!
    //Still not good
	void HealthShown ()
	{
        if (CurrentHealth > 1)
            Health1.SetActive(true);
        else Health1.SetActive(false);

        if (CurrentHealth > 2)
            Health2.SetActive(true);
        else Health2.SetActive(false);

        if (CurrentHealth > 3)
            Health3.SetActive(true);
        else Health3.SetActive(false);

        if (CurrentHealth > 4)
            Health4.SetActive(true);
        else Health4.SetActive(false);

        if (CurrentHealth > 5)
            Health5.SetActive(true);
        else Health5.SetActive(false);

        if (CurrentHealth > 6)
            Health6.SetActive(true);
        else Health6.SetActive(false);

        if (CurrentHealth > 7)
            Health7.SetActive(true);
        else Health7.SetActive(false);

        if (CurrentHealth >= 8)
            Health8.SetActive(true);
        else Health8.SetActive(false);
	}
}
