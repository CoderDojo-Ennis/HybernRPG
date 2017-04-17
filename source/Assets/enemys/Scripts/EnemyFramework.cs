using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFramework : MonoBehaviour {
    //Other
    private EnemyFramework enemyClass;  //Name of the class describing the enemy
                                        //e.g. "LaserCultist" and "SlappingCultist"
    public EnemyFramework EnemyClass { get; set; }
    
    //Stats in alphabetical order
    private int attack;             //Flat damage dealt
    private bool beam;              //Can use BeamAttack()
    private int maxHealth;          //Maximum amount of damage an enemy can withstand
    private bool melee;             //Can use MeleeAttack()
    private float minAttackRange;   //Minimum distance to attack (ranged)
    private float maxAttackRange;   //Maximum distance to attack (ranged)
    private int jumps;              //How many times can they jump
    private float jumpHeight;       //How high can they jump
    private bool projectile;        //Can use ProjectileAttack()
    private float speed;            //How fast they can move

    public int Attack { get; set; }
    public bool Beam { get; set; }
    public int MaxHealth { get; set; }
    public bool Melee { get; set; }
    public float MinAttackRange { get; set; }
    public float MaxAttackRange { get; set; }
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
}
