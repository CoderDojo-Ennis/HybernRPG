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
        HealthCurrent = 5;
        HealthMax = 5;
        Melee = false;
        Jumps = 1;
        JumpHeight = 0.5f;
        Projectile = true;
        Speed = 0.75f; 
    }
}
