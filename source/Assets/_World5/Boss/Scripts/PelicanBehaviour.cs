using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Confused? Ask Cian.
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
    */

    //Fight Management
    public bool ActionOngoing = false;
    public bool Defeated = false;
    //private PelicanFight fight;
    public GameObject player;

    //Health
    private Slider healthSlider;
    private Image fill;
    private Color phase1;
    private Color phase2;
    private Color phase3;

    //Navigation
    private Rigidbody2D rb;
    public GameObject NavPointContainer;
    public NavPoint LastNavPoint;
    public NavPoint TargetNavPoint;

    public NavPoint[] AllNavPoints;

    //This gives all of the relevant information about the Pelican, along with functions about how it behaves
    //These can change over the course of the battle
    private int StartDamage = 3;
    private int StartHealth = 120;
    private int StartPhase = 2;
    private int StartSpeed = 5;

    //Current Values
    public int CurrentDamage;
    public int CurrentHealth;
    public int CurrentPhase;
    public int CurrentSpeed;

    public void Start()
    {
        //Other
        rb = GetComponent<Rigidbody2D>();
        NavPointContainer = GameObject.Find("NavPoints");
        AllNavPoints = NavPointContainer.GetComponentsInChildren<NavPoint>();
        LastNavPoint = NavPoint.FindClosestNavPoint(this.transform.position, AllNavPoints);

        //PelicanFight
        //fight = transform.GetComponent<PelicanFight>();

        //Player
        player = GameObject.Find("Player Physics Parent");

        //Slider
        healthSlider = GameObject.Find("BossHealth").GetComponent<Slider>();
        healthSlider.value = StartHealth;
        //Slider Color
        fill = GameObject.Find("Fill").GetComponent<Image>();
        phase1 = new Color(1.00f, 0.17f, 1.00f);    //purple (bright)
        phase2 = new Color(0.75f, 0.17f, 0.75f);    //purple (medium)
        phase3 = new Color(0.55f, 0.17f, 0.55f);    //purple (dark)
        fill.color = phase1;

        //FIGHT!
        BeginFight();
    }

    public void Travel (Vector2 position)
    {
        Vector2 vector = position - rb.position;
        vector.Normalize();
        rb.velocity = vector * CurrentSpeed;
    }

    //Starts fight
    public void BeginFight()
    {
        CurrentDamage = StartDamage;
        CurrentHealth = StartHealth;
        CurrentSpeed = StartSpeed;
        CurrentPhase = StartPhase;
    }

    //Functions called during fight
    //Air Based attacks only
    //Phase One
    public void Peck ()
    {
        ActionOngoing = true;
        Debug.Log("Use Peck!");

        ActionOngoing = false;
    }

    public void WingAttack ()
    {
        ActionOngoing = true;
        Debug.Log("Use Wing Attack!");

        ActionOngoing = false;
    }

    public void HyperBeam ()
    {
        ActionOngoing = true;
        Debug.Log("Hyper Beam!");

        ActionOngoing = false;
    }

    //Phase Two
    public void Hurricane ()
    {
        ActionOngoing = true;
        Debug.Log("Use Hurricane!");

        ActionOngoing = false;
    }

    public void CallForHelp ()
    {
        ActionOngoing = true;
        Debug.Log("Call for help!");
        //int amount = Random.Range(1, 4);

        ActionOngoing = false;
    }

    public void SkyDrop ()
    {
        ActionOngoing = true;
        Debug.Log("Use Sky Drop!");

        ActionOngoing = false;
    }

    //Phase Three
    public void Dive ()
    {
        ActionOngoing = true;
        Debug.Log("Use Dive!");

        ActionOngoing = false;
    }

    public void Scald ()
    {
        ActionOngoing = true;
        Debug.Log("Use Scald!");

        ActionOngoing = false;
    }

    public void GigaImpact ()
    {
        ActionOngoing = true;
        Debug.Log("Use Giga Impact!");

        ActionOngoing = false;
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
}
