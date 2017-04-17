using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public LaserCultist()
    {
        Attack = 5;
        AttackRangeMax = 12f;
        AttackRangeMin = 4f;
        Beam = true;
        HealthCurrent = 4;
        HealthMax = 4;
        Melee = false;
        Jumps = 1;
        JumpHeight = 0.5f;
        Projectile = false;
        Speed = 2f;
    }
}
