using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public LaserCultist()
    {
        Attack = 4;
        Beam = true;
        MaxHealth = 4;
        Melee = false;
        MinAttackRange = 4f;
        MaxAttackRange = 10f;
        Jumps = 1;
        JumpHeight = 0.5f;
        Projectile = false;
        Speed = 8f;
    }
}
