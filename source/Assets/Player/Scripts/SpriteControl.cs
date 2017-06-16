using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControl : MonoBehaviour {
	
	//Sprites for normal body
	public Sprite head;
	public Sprite shoulder1;
	public Sprite shoulder2;
	public Sprite forearm1;
	public Sprite forearm2;
	public Sprite torso;
	public Sprite thigh1;
	public Sprite thigh2;
	public Sprite calf1;
	public Sprite calf2;
	
	//Sprites for swapped body parts
	///Sprites for Arms
	public Sprite shoulder1Pickaxe;
	public Sprite shoulder2Pickaxe;
	
	public Sprite shoulder2Shield;
	public Sprite forearm2Shield;
	
	public Sprite shoulder1GrapplingHook;
	public Sprite forearm1GrapplingHook;
	
	public Sprite shoulder1ArmCannon;
	public Sprite shoulder2ArmCannon;
	public Sprite forearm1ArmCannon;
	public Sprite forearm2ArmCannon;
	
	///sprites for Torso
	public Sprite torsoHeavy;
	
	
	
	void Start()
	{
		SetHead(head);
		SetShoulder1(shoulder1);
		SetShoulder2(shoulder2);
		SetForearm1(forearm1);
		SetForearm2(forearm2);
		SetTorso(torso);
		SetThigh1(thigh1);
		SetThigh2(thigh2);
		SetCalf1(calf1);
		SetCalf2(calf2);
		
	}
	public void SetSprites(int armLimbs, int torsoLimbs)
	{
		switch(armLimbs)
		{
			case 0: //Normal
			SetShoulder1(shoulder1);
			SetShoulder2(shoulder2);
			SetForearm1(forearm1);
			SetForearm2(forearm2);
			break;
			case 1:///Pickaxe
			SetShoulder1(shoulder1Pickaxe);
			SetShoulder2(shoulder2Pickaxe);
			SetForearm1(null);
			SetForearm2(null);
			break;
			case 2://Shield
			SetShoulder1(shoulder1);
			SetShoulder2(shoulder2Shield);
			SetForearm1(forearm1);
			SetForearm2(forearm2Shield);
			break;
			case 3://Grappling Hook
			SetShoulder1(shoulder1GrapplingHook);
			SetShoulder2(shoulder2);
			SetForearm1(forearm1GrapplingHook);
			SetForearm2(forearm2);
			break;
			case 7: //Arm cannon
			SetShoulder1(shoulder1ArmCannon);
			SetShoulder2(shoulder2ArmCannon);
			SetForearm1(forearm1ArmCannon);
			SetForearm2(forearm2ArmCannon);
			break;
		}
		switch(torsoLimbs)
		{
			case 0:
			SetTorso(torso);
			break;
			case 1:
			SetTorso(torsoHeavy);
			break;
		}
	}
	private void SetHead(Sprite sprite)
	{
		transform.Find("head").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetShoulder1(Sprite sprite)
	{
		transform.Find("Arms").Find("shoulder1").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetShoulder2(Sprite sprite)
	{
		transform.Find("Arms").Find("shoulder2").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetForearm1(Sprite sprite)
	{
		transform.Find("Arms").Find("shoulder1").Find("forearm1").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetForearm2(Sprite sprite)
	{
		transform.Find("Arms").Find("shoulder2").Find("forearm2").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetTorso(Sprite sprite)
	{
		transform.Find("torso").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetThigh1(Sprite sprite)
	{
		transform.Find("Legs").Find("Thigh1").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetThigh2(Sprite sprite)
	{
		transform.Find("Legs").Find("Thigh2").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetCalf1(Sprite sprite)
	{
		transform.Find("Legs").Find("Thigh1").Find("Calf1").GetComponent<SpriteRenderer>().sprite = sprite;
	}
	private void SetCalf2(Sprite sprite)
	{
		transform.Find("Legs").Find("Thigh2").Find("Calf2").GetComponent<SpriteRenderer>().sprite = sprite;
	}	
}
