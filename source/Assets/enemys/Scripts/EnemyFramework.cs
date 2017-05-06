using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFramework : MonoBehaviour {

    public GameObject LaserBeam;

    //Stats in alphabetical order
    //Most unimplemented
    private int attack;             //Flat damage dealt
    private float attackRangeMax;   //Minimum distance to attack (ranged)
    private float attackRangeMin;   //Maximum distance to attack (ranged)
    private bool beam;              //Can use BeamAttack()
    private int health;             //Maximum amount of damage an enemy can withstand
    private int jumps;              //How many times can they jump
    private float jumpHeight;       //How high can they jump
    private bool melee;             //Can use MeleeAttack()
    private bool projectile;        //Can use ProjectileAttack()
    private float speed;            //How fast they can move

    public int Attack { get; set; }
    public float AttackRangeMax { get; set; }
    public float AttackRangeMin { get; set; }
    public bool Beam { get; set; }
    public int Health { get; set; }
    public int Jumps { get; set; }
    public float JumpHeight { get; set; }
    public bool Melee { get; set; }
    public bool Projectile { get; set; }
    public float Speed { get; set; }

    //Default attack in close quarters
    public void MeleeAttack()
    {

    }
    
    //Default ranged attack affected by gravity
    public void ProjectileAttack()
    {

    }

    //Default ranged attack in straight line
    public void BeamAttack(GameObject target)
    {
        Component Control;
        Control = LaserBeam.GetComponent("LaserControl");
        //LaserBeam.getComponent("LaserControl").LaserBehaviour();
        Vector3 lastPos;
        Vector3 targetPos;
        float lerpMove = 0;
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position - transform.position, Color.red);
        Ray2D ray = new Ray2D(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position - transform.position);
        hit = new RaycastHit();
        lastPos = transform.position;
        targetPos = hit.point;
        lerpMove += Time.deltaTime;
        Instantiate(LaserBeam);
        //LaserBeam.transform.position = Vector3.MoveTowards(transform.position + new Vector3(0f, 0.6f, 0f), target.transform.position, 2 * Time.deltaTime);
        LaserBeam.transform.position = Vector3.MoveTowards(lastPos, targetPos, 10 * lerpMove);
        Debug.Log("Over");
        
    }
    //Only called in enemyBehaviour
    //'s' is equal to enemyType
    public EnemyFramework EnemyCreation(string s)
    {
        switch (s)
        {
            //Sorted alphabetically
            case "ArmCannonCultist":
                return new ArmCannonCultist();
            case "LaserCultist":
                return new LaserCultist();
            case "MeleeCultist":
                return new MeleeCultist();
            default:
                return null;
        }
    }
}
