using UnityEngine;

[System.Serializable]
public class NavNeighbor
{
    public NavPoint NeighborPoint;

    public TravelTypes TravelType;

    // public bool TwoWay;

    [HideInInspector]
    public float Weight;
}