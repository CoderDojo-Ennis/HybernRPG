using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCultist : EnemyFramework {
    //Sets variables from EnemyFramework
    public LaserCultist()
    {
        /*Attack = 5;
        AttackRangeMax = 0f;
        AttackRangeMin = 4f;
        Beam = true;
        Health = 4;
        Jumps = 1;
        JumpHeight = 0.5f;
        Melee = false;
        Projectile = false;
        Speed = 2f;*/
    }
	//Default ranged attack in straight line
    public void BeamAttack(GameObject target)
    {
        /*Component Control;
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
        Debug.Log("Over");*/
        
    }
}
