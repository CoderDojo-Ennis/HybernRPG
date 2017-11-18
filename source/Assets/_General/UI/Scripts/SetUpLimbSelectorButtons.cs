using UnityEngine;
using UnityEngine.UI;

public class SetUpLimbSelectorButtons : MonoBehaviour {

	private Button button;
	private ChangeLimb changeLimb;
	
	void Start () {
		changeLimb = GameObject.Find("Player Physics Parent").GetComponent<ChangeLimb>();
	}
	
	void OnEnable () {
		
		foreach( Transform child in transform)
		{
			button = child.gameObject.GetComponent<Button>();
			
			button.onClick.AddListener( delegate{ButtonClicked(child.gameObject.name);} );
			
		}
	}

	void ButtonClicked (string limb) {
		switch(limb)
		{
			case "Normal Arms":
				changeLimb.SwitchArms(0);
			break;
			case "Pickaxe":
				changeLimb.SwitchArms(1);
			break;
			case "Shield":
				changeLimb.SwitchArms(2);
			break;
			case "Grappling Hook":
				changeLimb.SwitchArms(3);
			break;
			case "Arm Cannon":
				changeLimb.SwitchArms(7);
			break;
			case "Normal Torso":
				changeLimb.SwitchTorso(0);
			break;
			case "Heavy Torso":
				changeLimb.SwitchTorso(1);
			break;
			case "Jetpack":
				changeLimb.SwitchTorso(2);
			break;
			case "Cactus":
				changeLimb.SwitchTorso(3);
			break;
		}
	}
}
 