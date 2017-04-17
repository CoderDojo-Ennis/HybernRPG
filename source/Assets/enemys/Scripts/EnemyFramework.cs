using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFramework : MonoBehaviour {
    //Other
    public EnemyFramework laserCultist;

    private EnemyFramework enemyClass;  //Name of the class describing the enemy
                                        //e.g. "LaserCultist" and "SlappingCultist"
    public EnemyFramework EnemyClass { get; set; }
    
    //Stats in alphabetical order
    private int attack;             //Flat damage dealt
    private float attackRangeMax;   //Minimum distance to attack (ranged)
    private float attackRangeMin;   //Maximum distance to attack (ranged)
    private bool beam;              //Can use BeamAttack()
    private int healthCurrent;      //Current amount of damage an enemy can withstand
    private int healthMax;          //Maximum amount of damage an enemy can withstand
    private bool melee;             //Can use MeleeAttack()
    private int jumps;              //How many times can they jump
    private float jumpHeight;       //How high can they jump
    private bool projectile;        //Can use ProjectileAttack()
    private float speed;            //How fast they can move

    public int Attack { get; set; }
    public float AttackRangeMax { get; set; }
    public float AttackRangeMin { get; set; }
    public bool Beam { get; set; }
    public int HealthCurrent { get; set; }
    public int HealthMax { get; set; }
    public bool Melee { get; set; }
    public int Jumps { get; set; }
    public float JumpHeight { get; set; }
    public bool Projectile { get; set; }
    public float Speed { get; set; }

    //Attack in close quarters
    void MeleeAttack()
    {

    }
    
    //Ranged attack affected by gravity
    void ProjectileAttack()
    {

    }

    //Ranged attack in straight line
    void BeamAttack()
    {

    }
    //Only called in enemyMove (Should probably be enemyBehaviour)
    //'s' is always equal to enemyType
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
