using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GetCoordinates
{
    //Coordinates for dictionary
    public int X { get; set; }
    public int Y { get; set; }

    public GetCoordinates(int x, int y)
    {
        this.X = x;
        this.Y = y;

    }
    //Part of a* algorithm
    public static bool operator ==(GetCoordinates first, GetCoordinates second)
    {
        return first.X == second.X && first.Y == second.Y;
    }
    public static bool operator !=(GetCoordinates first, GetCoordinates second)
    {
        return first.X != second.X || first.Y != second.Y;
    }
    public static GetCoordinates operator -(GetCoordinates x, GetCoordinates y)
    {
        return new GetCoordinates(x.X - y.X, x.Y - y.Y);
    }
}
