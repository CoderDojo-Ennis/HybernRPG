using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public LaserCultist()
    {
        Attack = 5;
        AttackRangeMax = 0f;
        AttackRangeMin = 4f;
        Beam = true;
        Health = 4;
        Jumps = 1;
        JumpHeight = 0.5f;
        Melee = false;
        Projectile = false;
        Speed = 2f;
    }
}
