using UnityEngine;

public class PelicanFight : PelicanBehaviour
{
    public void PelicanBehaviourPhaseOne()
    {
        //Peck 1 to 3 times
        Peck();
        //After pecking, WingAttack()
        WingAttack();
        Debug.Log("Finito");
    }

    public void PelicanBehaviourPhaseTwo()
    {

    }

    public void PelicanBehaviourPhaseThree()
    {

    }

    public void Update()
    {
        //GameObject bestMatch = player;
        if (CurrentPhase == 1)
        {
            //Travel(player.transform.position);
        }

        else if (CurrentPhase == 2)
        {
        
            NavPointPath path = FindPathToTarget(player.transform.position);
            if (path != null && path.Neighbors != null && path.Neighbors.Count > 0)
            {
                // Whats the next action
                var neighbor = path.Neighbors[0];
                //var neighborVector = neighbor.NeighborPoint.transform.position - this.transform.position;
                Travel(neighbor.NeighborPoint.transform.position);
            }
        }
    }

    private NavPointPath FindPathToTarget(Vector3 target)
    {
        TargetNavPoint = NavPoint.FindClosestNavPoint(target, this.AllNavPoints);
        Debug.Log(gameObject.name + " is dreaming of getting to " + TargetNavPoint.name);

        NavPoint from = LastNavPoint;
        NavPoint to = TargetNavPoint;
        NavPointPath BestPath = from.GetBestPath(to, this.AllNavPoints);
        return BestPath;
    }
}