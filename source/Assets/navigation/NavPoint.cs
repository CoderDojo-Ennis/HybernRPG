using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NavPoint : MonoBehaviour
{
    public NavNeighbor[] Neighbors;

    private Dictionary<int, NavPointPath> BestPaths = new Dictionary<int, NavPointPath>();
    private Dictionary<string, NavPointPath> AllPaths = new Dictionary<string, NavPointPath>();

    public int Id
    {
        get
        {
            return this.GetInstanceID();
        }
    }

    // Get the best path from the cache - or if it's not there, find it
    public NavPointPath GetBestPath(NavPoint targetPoint, NavPoint[] allNavPoints)
    {
        NavPointPath bestPath;

        // Have we already solved this problem?
        int key = targetPoint.Id;
        if (this.BestPaths.ContainsKey(key))
        {
            bestPath = this.BestPaths[key];
        } else
        {
            bestPath = FindBestPath(this, targetPoint);
        }

        return bestPath;
    }

    // Find all possible paths from this point, that don't intersect
    private Dictionary<string, NavPointPath> FindAllPaths()
    {
        var allPaths = new Dictionary<string, NavPointPath>();
        foreach(var neighbor in this.Neighbors) {
            // Weight calculation between two points
            if (neighbor.Weight == 0)
            {
                neighbor.Weight = DistanceTo(neighbor.NeighborPoint);
            }

            // Create a simple path to just the next neighbor
            var path = new NavPointPath(this, neighbor);
            allPaths.Add(path.Key, path);

            // Recurse - mix in all paths from that neighbor - excluding points already hit
            var childPaths = neighbor.NeighborPoint.FindAllPathsExcluding(path);
            foreach(var childPath in childPaths)
            {
                allPaths.Add(childPath.Key, childPath.Value);
            }
        }

        return allPaths;
    }

    // Find all child paths excluding ones that hit points we've already hit
    private Dictionary<string, NavPointPath> FindAllPathsExcluding(NavPointPath pathStart)
    {
        var allPaths = new Dictionary<string, NavPointPath>();
        foreach (var neighbor in this.Neighbors)
        {
            if (!pathStart.ContainsPoint(neighbor.NeighborPoint))
            {
                // Weight calculation between two points
                if (neighbor.Weight == 0)
                {
                    neighbor.Weight = DistanceTo(neighbor.NeighborPoint);
                }

                // Create a simple path to just the next neighbor
                var path = pathStart.CopyAdd(neighbor);
                allPaths.Add(path.Key, path);

                // Recurse - mix in all paths from that neighbor - excluding points already hit
                var childPaths = neighbor.NeighborPoint.FindAllPathsExcluding(path);
                foreach (var childPath in childPaths)
                {
                    allPaths.Add(childPath.Key, childPath.Value);
                }
            }
        }

        return allPaths;
    }

    // Find the best of all the paths
    private NavPointPath FindBestPath(NavPoint from, NavPoint to)
    {
        // One time generation of all possible paths from this point
        if (this.AllPaths == null || this.AllPaths.Count == 0) {
            this.AllPaths = FindAllPaths();

            foreach (var path in this.AllPaths) {
                Debug.Log(path.Key);
            }
        }

        // Pick the ones that gets us to the destination
        float bestDistance = float.MaxValue;
        NavPointPath bestPath = null;
        foreach(var pathKeyVal in this.AllPaths)
        {
            if (pathKeyVal.Value.PointIds.Last() == to.Id)
            {
                // Is this the shortest distance
                if (pathKeyVal.Value.TotalDistance < bestDistance)
                {
                    bestDistance = pathKeyVal.Value.TotalDistance;
                    bestPath = pathKeyVal.Value;
                }
            }
        }

        // No paths get to the destination
        if (bestPath == null)
        {
            // We could pick the one that gets closest to the target
        }

        return bestPath;
    }

    /// <summary>
    /// Get the distance to another nav point
    /// </summary>
    /// <param name="otherPoint">The other point</param>
    /// <returns>Magnitude of the vector between them</returns>
    public float DistanceTo(NavPoint otherPoint)
    {
        return (otherPoint.transform.position - this.transform.position).magnitude;
    }

    // todo - move to nav manager
    public static NavPoint FindClosestNavPoint(Vector3 search, NavPoint[] allNavPoints)
    {
        float bestDistance = float.MaxValue;
        NavPoint best = null;
        foreach (var point in allNavPoints)
        {
            float distance = (point.transform.position - search).magnitude;
            if (distance < bestDistance)
            {
                bestDistance = distance;
                best = point;
            }
        }
        return best;
    }

    public void SetBestPath(NavPoint targetPoint, NavPointPath path)
    {
        this.BestPaths[targetPoint.Id] = path;
    }

    void OnDrawGizmos()
    {
        DrawGizmoLines(Color.yellow, Color.yellow);
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmoLines(Color.white, Color.green);
    }

    void DrawGizmoLines(Color dotColor, Color lineColor)
    {
        Gizmos.color = dotColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = lineColor;
        if (Neighbors != null)
        {
            foreach (var neighbor in Neighbors)
            {
                if (neighbor.NeighborPoint != null)
                {
                    Vector3 heading = neighbor.NeighborPoint.transform.position - transform.position;
                    float dist = heading.magnitude;
                    if (neighbor.TravelType == TravelTypes.Jump)
                    {
                        heading.y += dist / 3;
                    }
                    Vector3 dir = heading / dist;
                    Gizmos.DrawRay(transform.position, dir * (dist / 3f));
                }
            }
        }
    }
}
