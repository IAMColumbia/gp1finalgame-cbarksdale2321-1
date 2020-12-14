using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UnityGridManager : ReferenceClass<UnityGridManager>
{
    //referenced StackOverflow and Unity Support

    [SerializeField] private GameObject[] tiles;
    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }


    }
    public Dictionary<GetCoordinates, Tile> Tiles { get; protected set; }
    int gridX;
    int gridY;
    [SerializeField] private Movement cameraMovement;

    [SerializeField] private Transform map;
    private GetCoordinates mapSize;
    protected virtual void Start()
    {
        CreateGrid();

    }
    public Castle Castle { get; set; }
    public SpawnManager MonsterSpawn { get; set; }
    private GetCoordinates enemySpawn;
    private GetCoordinates castleSpawn;
    [SerializeField]
    public GameObject enemySpawnPoint;
    [SerializeField]
    private GameObject castleSpawnPoint;

    private Stack<Node> path;
    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }

    //Makes a string[] that indexes all the chars in the text file then for each char in the text file, the char read is turned into a indexer for the block array(changes block value)
    protected virtual void CreateGrid()
    {
        //Dictionary method taken from Algorithms and inScope
        Tiles = new Dictionary<GetCoordinates, Tile>();
        
        string[] mapData = ReadMapText();
        mapSize = new GetCoordinates(mapData[0].ToCharArray().Length, mapData.Length);
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 gridStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

       for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
               PlaceTile(newTiles[x].ToString(),x, y, gridStartPosition);

            }
        }
        maxTile = Tiles[new GetCoordinates(mapX - 1, mapY - 1)].transform.position;

        cameraMovement.SetBounds(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        GetSpawnPoints();
    }

    protected virtual void PlaceTile(string tileType,int x, int y, Vector3 gridStartPosition)
    {
        int tileIndex = int.Parse(tileType);

        Tile newTile = Instantiate(tiles[tileIndex]).GetComponent<Tile>();
        
        newTile.Setup(new GetCoordinates(x,y), newTile.transform.position = new Vector3(gridStartPosition.x + (TileSize * x), gridStartPosition.y - (TileSize * y), 0), map);

       

        
    }
    //Thank God for google
    private string[] ReadMapText()
    {
        TextAsset data = Resources.Load("LevelEditor") as TextAsset;

        string tempData = data.text.Replace(Environment.NewLine, string.Empty);

        return tempData.Split('-');
    }
    protected virtual void GetSpawnPoints()
    {
        enemySpawn = new GetCoordinates(0, 0);
        GameObject tmp = Instantiate(enemySpawnPoint, Tiles[enemySpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);
       MonsterSpawn = tmp.GetComponent<SpawnManager>();
        MonsterSpawn.name = "MossBlock";

        castleSpawn = new GetCoordinates(15, 4);
        Instantiate(castleSpawnPoint, Tiles[castleSpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);

    }

    public bool InGrid(GetCoordinates position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }

    public void GeneratePath()
    {
        path = PathAlgorithm.GetPath(enemySpawn, castleSpawn);
    }




}
