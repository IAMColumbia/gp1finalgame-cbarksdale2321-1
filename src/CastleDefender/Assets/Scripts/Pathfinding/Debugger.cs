using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private Tile goal;
    [SerializeField]private Tile start;

    [SerializeField] private GameObject debugTilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* Update is called once per frame
    void Update()
    {
        ClickTile();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PathAlgorithm.GetPath(start.GridPosition, goal.GridPosition);
        }
    }
    */

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider !=null)
            {
                Tile tmp = hit.collider.GetComponent<Tile>();
                if (tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        PlaceDebugger(start.WorldPosition, new Color32(255, 135, 0, 255));
                    }
                    else if(goal == null)
                    {
                        goal = tmp;
                        PlaceDebugger(goal.WorldPosition, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList , HashSet<Node> closedList, Stack<Node> path)
    {
        foreach (Node node in openList)
        {
            if (node.TileReference != start && node.TileReference != goal)
            {
                PlaceDebugger(node.TileReference.WorldPosition, Color.cyan ,node);
            }
           
           
        }
        foreach (Node node in closedList)
        {
            if (node.TileReference != start && node.TileReference != goal && path.Contains(node) )
            {
                PlaceDebugger(node.TileReference.WorldPosition, Color.blue, node);
            }


        }
        foreach (Node node in path)
        {
            if (node.TileReference != start && node.TileReference != goal)
            {
                PlaceDebugger(node.TileReference.WorldPosition, Color.green, node);
            }
        }
    }



    public void PlaceDebugger(Vector3 worldPos, Color32 color, Node node = null)
    {
        GameObject debugTile = Instantiate(debugTilePrefab, worldPos, Quaternion.identity);

        if (node != null)
        {
            //debugTile.GetComponent<DebugTile>().G.text += node.G;
            Debug.Log("G Value: " + node.G);
            Debug.Log("H Value: " + node.H);
            Debug.Log("F Value: " + node.F);
        }

        debugTile.GetComponent<SpriteRenderer>().color = color;

    }
}
