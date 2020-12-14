using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public GetCoordinates GridPosition { get; protected set; }

    public Tile TileReference { get; private set; }

    public Node Parent { get; private set; }

    public Vector2 WorldPosition { get; set; }

    public int G { get; set; }

    public int H { get; set; }
    public int F { get; set; }

    public Node(Tile tileReference)
    {
        this.TileReference = tileReference;
        this.GridPosition = tileReference.GridPosition;
        this.WorldPosition = tileReference.WorldPosition;
    }

    public void GetValues(Node parent, Node goal, int gCost)
    {
        this.Parent = parent;
        this.G = parent.G + gCost;
        this.H = Mathf.Abs(((GridPosition.X - goal.GridPosition.X) + Mathf.Abs(goal.GridPosition.Y - GridPosition.Y)) * 10);
        this.F = G + H;
    }
 
}
