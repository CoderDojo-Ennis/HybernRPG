using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public MeleeCultist()
    {
        Attack = 3;
        AttackRangeMax = 0.5f;
        AttackRangeMin = 0.5f;
        Beam = false;
        HealthCurrent = 3;
        HealthMax = 3;
        Melee = true;
        Jumps = 2;
        JumpHeight = 1f;        //Based off my test results, I have the following conclusions:
        Projectile = false;     //if you up the speed to 20 it becomes the Flash. Extremely quick, flickers a bit when moving, very difficult.
        Speed = 2f;            //if you up the speed to 100 it becomes Goku. Flickers constantly when moving, moves through objects, jumps into the next solar system and is absolutely unstoppable.
    }
}
