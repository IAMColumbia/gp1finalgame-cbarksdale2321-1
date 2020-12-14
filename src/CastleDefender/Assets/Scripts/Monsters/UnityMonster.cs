using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityMonster : MonoBehaviour
{
    [SerializeField] private float speed;
    private Stack<Node> path;
    public GetCoordinates GridPosition { get; set; }
    public bool isActive;

    private Vector3 destination;
    private int lives;
    [SerializeField] private Text livesText;


    [SerializeField]
    private GameObject[] monsterPrefabs;
    [SerializeField] private Stat health;
    public bool IsAlive
    {
        get { return health.CurrentValue > 0; }
    }

    public int timeBetweenAttacks;

    private void Awake()
    {
        health.Initialize();
    }
    private void Update()
    {
        Move();
    }

    public void Spawn(int health)
    {
        this.isActive = true;
        this.health.Bar.ResetHealth();
        this.health.MaxVal = health;
        this.health.CurrentValue = this.health.MaxVal;
        this.transform.position = UnityGridManager.Instance.MonsterSpawn.transform.position;
        SetPath(UnityGridManager.Instance.Path);
    }

    private void Move()
    {
        if (isActive)
        {

            GetComponent<SpriteRenderer>().sortingOrder = 0;
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }
    private void SetPath(Stack<Node> newPath)
    {
       
        if (newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Castle")
        {
            GameManager.Instance.lives = GameManager.Instance.lives -1;
            GameManager.Instance.livesText.text = string.Format("Lives: <color=lime>{0}</color>", GameManager.Instance.lives);
            
            Release();
        }
    }
    private void Release()
    {
        isActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage(int damage)
    {
        if (isActive)
        {
            health.CurrentValue -= damage;

            if (health.CurrentValue <= 0)
            {
                GameManager.Instance.Currency += 2;
                GetComponent<UnityMonster>().Release();
                GetComponent<SpriteRenderer>().sortingOrder--;

                
            }
        }
        
        
    }

}
