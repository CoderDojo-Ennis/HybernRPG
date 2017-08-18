using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PelicanBehaviour : MonoBehaviour
{
    /*  Phase One
     * Peck()
     * WingAttack()
     * HyperBeam()
     * 
     *  Phase Two
     * Hurricane()
     * CallForHelp()
     * SkyDrop()
     * 
     *  Phase Three
     * Dive()
     * Scald()
     * GigaImpact()
     * 
     */
    private bool Defeated = false;
    private PelicanFight fight;
    private Slider healthSlider;
    private Image fill;
    private Color phase1;   //magenta to red
    private Color phase2;   //yellow to green
    private Color phase3;
    //This gives all of the relevant information about the Pelican, along with functions about how it behaves
    //These can change over the course of the battle
    private int StartDamage = 3;
    private int StartHealth = 120;
    private int StartPhase = 1;
    private int StartSpeed = 5;

    //Current Values
    public int CurrentDamage;
    public int CurrentHealth;
    public int CurrentPhase;
    public int CurrentSpeed;

    public void Awake()
    {
        //PelicanFight
        fight = transform.GetComponent<PelicanFight>();

        //Slider
        healthSlider = GameObject.Find("BossHealth").GetComponent<Slider>();
        healthSlider.value = StartHealth;
        //Slider Color
        fill = GameObject.Find("Fill").GetComponent<Image>();
        phase1 = new Color(1.00f, 0.17f, 1.00f);    //purple (bright)
        phase2 = new Color(0.66f, 0.17f, 0.66f);    //purple (medium)
        phase3 = new Color(0.33f, 0.17f, 0.33f);    //purple (dark)
        fill.color = phase1;

        //FIGHT!
        BeginFight();
    }

    IEnumerator Wait(int s)
    {
        yield return new WaitForSeconds(s);
    }

    //Starts fight
    public void BeginFight()
    {
        CurrentDamage = StartDamage;
        CurrentHealth = StartHealth;
        CurrentSpeed = StartSpeed;
        CurrentPhase = StartPhase;
        fight.Think();
    }

    //Functions called during fight
    //Air Based unless stated otherwise
    public void Peck ()
    {
        //Ground Based
    }

    public void WingAttack ()
    {
        //Ground to air Based
    }

    public void HyperBeam ()
    {
        
    }


    public void Hurricane()
    {
        
    }

    public void CallForHelp(int amount = 2)
    {
        amount = Random.Range(1, 4);
        
    }

    public void SkyDrop()
    {
        
    }


    public void Dive()
    {
        
    }

    public void Scald()
    {
        
    }

    public void GigaImpact()
    {
        
    }

    //Damage
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        print(CurrentHealth);
        healthSlider.value = (float)CurrentHealth / StartHealth;
        while (!Defeated)
        {
            if (CurrentHealth >= 90)
            {
                CurrentPhase = 1;
                fill.color = phase1;
                break;
            }
            else if (CurrentHealth >= 50)
            {
                CurrentPhase = 2;
                fill.color = phase2;
                break;
            }
            else if (CurrentHealth >= 1)
            {
                CurrentPhase = 3;
                fill.color = phase3;
                break;
            }

            //Death
            if (CurrentHealth <= 0)
            {
                EndFight();
                fill.gameObject.SetActive(false);
                break;
            }
        }
    }

    //Ends the battle, triggered when Pelican is defeated
    public void EndFight ()
    {
        Defeated = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
}
