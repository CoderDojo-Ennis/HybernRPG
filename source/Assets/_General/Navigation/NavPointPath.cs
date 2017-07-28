using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavPointPath
{
    public float TotalDistance;

    public NavPoint StartPoint;

    public List<NavNeighbor> Neighbors;

    private string key = null;

    public string Key
    {
        get
        {
            if (key == null && PointIds != null)
            {
                var idStrings = PointIds.ToArray().Select(p => p.ToString()).ToArray();
                key = string.Join(",", idStrings);
            }
            return key;
        }
    }

    public List<int> PointIds;

    public NavPointPath(NavPoint startPoint, NavNeighbor firstNeighbor)
    {
        this.StartPoint = startPoint;
        this.PointIds = new List<int>
        {
            startPoint.Id
        };
        this.Neighbors = new List<NavNeighbor>
        {
            firstNeighbor
        };
        this.PointIds.Add(firstNeighbor.NeighborPoint.Id);
        this.TotalDistance = firstNeighbor.Weight;
        this.key = null;
    }

    public void AddNeighbor(NavNeighbor nextNeighbor)
    {
        this.Neighbors.Add(nextNeighbor);
        this.PointIds.Add(nextNeighbor.NeighborPoint.Id);
        this.TotalDistance += nextNeighbor.Weight;
        this.key = null;
    }

    public bool ContainsPoint(NavPoint point)
    {
        return ContainsPointId(point.Id);
    }

    public bool ContainsPointId(int pointId)
    {
        return this.PointIds.Contains(pointId);
    }

    public bool ContainsAny(IEnumerable<int> pointIds)
    {
        return pointIds.Any(pid => this.ContainsPointId(pid));
    }

    public NavPointPath CopyAdd(NavNeighbor nextNeighbor)
    {
        var newPath = new NavPointPath(this.StartPoint, this.Neighbors[0]);
        for (var i = 1; i < this.Neighbors.Count; i++)
        {
            newPath.AddNeighbor(this.Neighbors[i]);
        }
        newPath.AddNeighbor(nextNeighbor);
        return newPath;
    }

    /*
    public static NavPointPath Add(NavPointPath pathA, NavPointPath pathB)
    {
        var newPath = new NavPointPath(pathA.StartPoint, pathA.Neighbors[0]);
        for (var i = 1; i < pathA.Neighbors.Count; i++)
        {
            newPath.AddNeighbor(pathA.Neighbors[i]);
        }
        for (var i = 0; i < pathB.Neighbors.Count; i++)
        {
            newPath.AddNeighbor(pathB.Neighbors[i]);
        }
        newPath.key = null;

        return newPath;
    }
    */
}
