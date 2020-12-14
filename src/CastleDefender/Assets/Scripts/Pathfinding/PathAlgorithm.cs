using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathAlgorithm
{
    //A* algorithm pattern referenced
    private static Dictionary<GetCoordinates, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<GetCoordinates, Node>();
        foreach (Tile tile in UnityGridManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static Stack<Node> GetPath(GetCoordinates start, GetCoordinates goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }
        //Thank you algorithms class
        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = nodes[start];

        openList.Add(currentNode);
        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    GetCoordinates neighborPos = new GetCoordinates(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);
                    if (UnityGridManager.Instance.InGrid(neighborPos) && UnityGridManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if (Math.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!CheckDiagonals(currentNode, nodes[neighborPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }
                        Node neighbor = nodes[neighborPos];



                        if (openList.Contains(neighbor))
                        {
                            if (currentNode.G + gCost < neighbor.G)
                            {
                                neighbor.GetValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                            neighbor.GetValues(currentNode, nodes[goal], gCost);
                        }
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //sorting list and getting lowest value
            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }
            if (currentNode == nodes[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }
        }

        return finalPath;
        //Debugging...
        //GameObject.Find("Debugger").GetComponent<Debugger>().DebugPath(openList, closedList, finalPath);
    }

    private static bool CheckDiagonals(Node currentNode, Node neighbor)
    {
        GetCoordinates direction = neighbor.GridPosition - currentNode.GridPosition;

        GetCoordinates firstCheck = new GetCoordinates(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y + direction.Y);

        GetCoordinates secondCheck = new GetCoordinates(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (UnityGridManager.Instance.InGrid(firstCheck) && !UnityGridManager.Instance.Tiles[firstCheck].Walkable)
        {
            return false;
        }
        if (UnityGridManager.Instance.InGrid(secondCheck) && !UnityGridManager.Instance.Tiles[secondCheck].Walkable)
        {
            return false;
        }
        return true;
    }
}
