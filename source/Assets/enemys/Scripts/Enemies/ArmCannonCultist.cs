using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCannonCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public ArmCannonCultist()
    {
        Attack = 2;
        AttackRangeMax = 0f; //5f
        AttackRangeMin = 2f;
        Beam = false;
        Health = 5;
        Jumps = 1;
        JumpHeight = 0.5f;
        Melee = false;
        Projectile = true;
        Speed = 0.75f; 
    }
}
