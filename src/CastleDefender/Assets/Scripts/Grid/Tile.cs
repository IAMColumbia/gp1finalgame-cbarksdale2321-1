using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public GetCoordinates GridPosition { get; protected set; }
    public bool isEmpty { get; set; }

    private UnityArcher myArcher;

    private Color32 fullTileColor = new Color32(255,118,118,255);
    private Color32 emptyTileColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

   

    public bool DebugTMP { get; set; }
    public bool Walkable { get; set; }

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2 WorldPosition
    {

        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y/2);
        }
    }
    
    
    public void Setup(GetCoordinates gridPos, Vector3 worldPosition, Transform parent)
    {
        Walkable = true;
        isEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
        UnityGridManager.Instance.Tiles.Add(gridPos, this);
        
    }

    private void OnMouseOver()
    {

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (isEmpty && !DebugTMP)
            {
                CheckColorTile(emptyTileColor);
            }
            if (!isEmpty && !DebugTMP)
            {
                CheckColorTile(fullTileColor);
            }

            else if (Input.GetMouseButtonDown(0))
            {
                PlaceArcher();
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButtonDown(0))
        {
            if (myArcher != null)
            {
                GameManager.Instance.SelectArcher(myArcher);
            }
            else
            {
                GameManager.Instance.DeselectArcher();
            }
        }
        
        


     
    }
    private void OnMouseExit()
    {
        if (!DebugTMP)
        {
            CheckColorTile(Color.white);
        }
        
    }

    private void PlaceArcher()
    {
       
        GameObject archer = Instantiate(GameManager.Instance.ClickedBtn.ArcherPrefab, transform.position, Quaternion.identity);

        archer.transform.SetParent(transform);
        isEmpty = false;
        CheckColorTile(Color.white);
        GameManager.Instance.BuyArcher();
        Walkable = false;
        

        this.myArcher = archer.transform.GetChild(0).GetComponent<UnityArcher>();

        myArcher.Price = GameManager.Instance.ClickedBtn.Price;
    }

    private void CheckColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
